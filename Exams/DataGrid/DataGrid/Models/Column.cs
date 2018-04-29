
namespace DataGrid.Models
{
    using System;
    using System.Collections.Generic;

    public abstract class Column
    {
        protected Column(TypeCode typeCode, string name)
        {
            this.TypeCode = typeCode;
            this.Name = name;
        }

        public TypeCode TypeCode { get; }

        public string Name { get; }
    }
}
