namespace DataGrid.Models.Generics
{
    using System;
    using System.Collections.Generic;

    public class Column<T> : Column, IColumn<T>
        where T : IComparable
    {
        public Column(TypeCode typeCode, string name, List<T> content)
            : base(typeCode, name)
        {
            this.Content = content;
        }

        public List<T> Content { get; }
    }
}
