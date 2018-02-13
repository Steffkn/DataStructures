using System;
using System.Collections.Generic;

public class AStar
{
    private char[,] maze;
    private PriorityQueue<Node> pQue;
    private Dictionary<Node, Node> parents;
    private Dictionary<Node, int> gCost;

    public AStar(char[,] map)
    {
        this.maze = map;
        pQue = new PriorityQueue<Node>();
        parents = new Dictionary<Node, Node>();
        gCost = new Dictionary<Node, int>();
    }

    public static int GetH(Node current, Node goal)
    {
        var deltaCol = Math.Abs(current.Col - goal.Col);
        var deltaRow = Math.Abs(current.Row - goal.Row);

        return deltaCol + deltaRow;
    }

    public IEnumerable<Node> GetPath(Node start, Node goal)
    {
        if (!gCost.ContainsKey(goal))
        {
            pQue = new PriorityQueue<Node>();
            parents.Clear();
            gCost.Clear();

            pQue.Enqueue(start);
            parents.Add(start, null);
            gCost.Add(start, 0);

            while (pQue.Count > 0)
            {
                var current = pQue.Dequeue();
                if (current.Equals(goal))
                {
                    break;
                }

                List<Node> neightbors = GetNearByNodes(current);
                int newCost = gCost[current] + 1;

                foreach (var node in neightbors)
                {
                    if (!gCost.ContainsKey(node) || newCost < gCost[node])
                    {
                        node.F = newCost + GetH(node, goal);
                        parents[node] = current;
                        gCost[node] = newCost;
                        pQue.Enqueue(node);
                    }
                }
            }
        }

        return this.ReconstructPath(parents, start, goal);
    }

    private IEnumerable<Node> ReconstructPath(Dictionary<Node, Node> parents, Node start, Node goal)
    {
        if (!parents.ContainsKey(goal))
        {
            return new List<Node>()
            {
                start
            };
        }

        Node current = parents[goal];
        var resultList = new Stack<Node>();
        resultList.Push(goal);

        while (!current.Equals(start))
        {
            resultList.Push(current);
            current = parents[current];
        }

        resultList.Push(start);
        return resultList;
    }

    private List<Node> GetNearByNodes(Node current)
    {
        int row = current.Row;
        int col = current.Col;

        int rowUp = row - 1;
        int rowDown = row + 1;
        int colLeft = col - 1;
        int colRight = col + 1;

        var nodeList = new List<Node>();
        this.AddToQueue(nodeList, rowUp, col);
        this.AddToQueue(nodeList, row, colRight);
        this.AddToQueue(nodeList, rowDown, col);
        this.AddToQueue(nodeList, row, colLeft);

        return nodeList;
    }

    private void AddToQueue(List<Node> nodeList, int row, int col)
    {
        if (IsInBounds(row, col) && !IsWall(row, col))
        {
            var newNode = new Node(row, col);
            nodeList.Add(newNode);
        }
    }

    private bool IsInBounds(int row, int col)
    {
        return row >= 0 && row < maze.GetLength(0)
               && col >= 0 && col < maze.GetLength(1);
    }

    private bool IsWall(int row, int col)
    {
        return this.maze[row, col] == 'W';
    }
}

