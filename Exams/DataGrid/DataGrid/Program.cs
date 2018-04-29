using System.Collections;

namespace DataGrid
{
    using System;
    using System.Collections.Generic;
    using DataGrid.Models;

    public class Program
    {
        private static Dictionary<TypeCode, List<Column>> grid = new Dictionary<TypeCode, List<Column>>();
        private static List<Tuple<TypeCode, int>> gridByColumns = new List<Tuple<TypeCode, int>>();
        private static int rowCount = 0;
        private static int columnCount = 0;
        private static ColumnFactory columnFactory = new ColumnFactory();

        public static void Main()
        {
            var genders = new List<string>()
            {
                "Male", "Female"
            };

            var ages = new List<int>()
            {
                20, 18
            };

            var kgs = new List<double>()
            {
                90.0, 60.0
            };
            rowCount = 2;
            AddColumn(TypeCode.Boolean, "gender", genders);
            AddColumn(TypeCode.Int32, "Age", ages);
            AddColumn(TypeCode.Double, "Kg", kgs);

            foreach (var gridByColumn in gridByColumns)
            {
                Console.Write(grid[gridByColumn.Item1][gridByColumn.Item2].Name + "\t");
            }

            Console.WriteLine();

            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < columnCount; col++)
                {
                    var currentColumn = gridByColumns[col];
                    Console.Write(grid[currentColumn.Item1][currentColumn.Item2].Content[row] + "\t");
                }

                Console.WriteLine();
            }
        }

        private static void AddColumn(TypeCode typeCode, string name, List<IComparable> items)
        {
            var newCol = columnFactory.CreateColumn(typeCode, name);

            if (!grid.ContainsKey(typeCode))
            {
                var newTypeColumns = new List<Column>();
                newTypeColumns.Add(newCol);
                grid.Add(typeCode, newTypeColumns);
                gridByColumns.Add(new Tuple<TypeCode, int>(typeCode, newTypeColumns.Count - 1));
            }
            else
            {
                grid[typeCode].Add(newCol);
                gridByColumns.Add(new Tuple<TypeCode, int>(typeCode, grid[typeCode].Count - 1));
            }

            columnCount++;
        }
    }
}
