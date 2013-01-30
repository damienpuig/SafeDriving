using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Direction.Helpers.Table
{
    public class TableColumn<T>
    {
        public string ColumnLabel { get; set; }
        public string ColumnUniqName { get; set; }
        public object ColumnHtmlOptions { get; set; }
        public string ColumnPropertyName { get; set; }
        public bool IsAction { get; set; }
        public TableActionLink<T> Action { get; set; }
    }
}