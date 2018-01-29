using System;
using System.Collections.Generic;
using System.Linq;

namespace _07.DistanceInLabyrinth
{
    public class DistanceInLabyrinth
    {
        private static List<Tuple<int, int>> Points = new List<Tuple<int, int>>();

        public static void Main()
        {
            int matrixSize = int.Parse(Console.ReadLine());
            string[,] matrix = new string[matrixSize, matrixSize];
            int startRow = -1;
            int startCol = -1;

            for (int i = 0; i < matrixSize; i++)
            {
                var row = Console.ReadLine();
                for (int j = 0; j < matrixSize; j++)
                {
                    matrix[i, j] = row[j].ToString();

                    if (row[j].Equals('*'))
                    {
                        startRow = i;
                        startCol = j;
                    }
                }
            }

            CheckNeighbours(matrix, startRow, startCol);
            int index = 1;

            while (Points.Count > 0)
            {
                var currentPoint = Points.Distinct().ToList();
                Points = new List<Tuple<int, int>>();
                foreach (var item in currentPoint)
                {
                    matrix[item.Item1, item.Item2] = (index).ToString();
                    CheckNeighbours(matrix, item.Item1, item.Item2);
                }
                index++;
            }

            Console.WriteLine();
            PrintMatrix(matrix);
        }

        private static void CheckNeighbours(string[,] matrix, int row, int col)
        {
            bool go1 = MarkMatrix(matrix, row - 1, col);
            bool go2 = MarkMatrix(matrix, row, col + 1);
            bool go3 = MarkMatrix(matrix, row + 1, col);
            bool go4 = MarkMatrix(matrix, row, col - 1);

            if (go1)
            {
                Points.Add(new Tuple<int, int>(row - 1, col));
            }

            if (go2)
            {
                Points.Add(new Tuple<int, int>(row, col + 1));
            }

            if (go3)
            {
                Points.Add(new Tuple<int, int>(row + 1, col));
            }

            if (go4)
            {
                Points.Add(new Tuple<int, int>(row, col - 1));
            }
        }

        public static bool MarkMatrix(string[,] matrix, int row, int col)
        {
            if (IsInMatrix(matrix, row, col))
            {
                if (matrix[row, col].Equals("0"))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsInMatrix(string[,] matrix, int row, int col)
        {
            return (row >= 0 && row < matrix.GetLength(0)) &&
                   col >= 0 && col < matrix.GetLength(1);
        }

        public static void PrintMatrix(string[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j].Equals("0"))
                    {
                        Console.Write("{0}", "u");
                    }
                    else
                    {
                        Console.Write("{0}", matrix[i, j]);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
