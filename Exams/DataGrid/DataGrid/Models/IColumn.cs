using System;
using System.Collections.Generic;

namespace DataGrid.Models
{
    public interface IColumn<T>
    {
        TypeCode TypeCode { get; }
        string Name { get; }
        List<T> Content { get; }
    }
}