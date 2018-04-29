using System;
using System.Collections.Generic;
using DataGrid.Models;
using DataGrid.Models.Generics;

namespace DataGrid
{
    public class ColumnFactory
    {
        public Column CreateColumn(TypeCode typeCode, string name)
        {
            switch (typeCode)
            {
                case TypeCode.Boolean:
                    return new Column<bool>(typeCode, name, new List<bool>());
                case TypeCode.Int32:
                    return new Column<int>(typeCode, name, new List<int>());
                case TypeCode.Double:
                    return new Column<double>(typeCode, name, new List<double>());
                case TypeCode.Decimal:
                    return new Column<decimal>(typeCode, name, new List<decimal>());
                //case TypeCode.String:
                //    return new Column<string>(typeCode, name, new List<string>());
                default:
                    throw new ArgumentException();
            }
        }
    }
}
