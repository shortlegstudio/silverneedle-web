// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Serialization
{
    using System.Collections.Generic;
    using System.Linq;
    public class DataTable : IGatewayObject
    {
        public string Name { get; private set; }
        private IList<string> columns;
        private IDictionary<string, string[]> rowData = new Dictionary<string, string[]>();
        public DataTable(string name)
        {
            this.Name = name;
        }
        public DataTable(IObjectStore configuration)
        {
            Name = configuration.GetString("name");
            columns = configuration.GetList("columns").ToList();
            var table = configuration.GetObject("table");

            foreach(var rowKey in table.Keys)
            {
                rowData.Add(rowKey, table.GetList(rowKey));
            }
        }

        public string Get(string rowKey, string column)
        {
            var colIndex = columns.IndexOf(column);
            var requestedRow = rowData[rowKey];
            return requestedRow[colIndex];
        }

        public void SetColumns(IEnumerable<string> columns)
        {
            this.columns = columns.ToList();
        }

        public void AddRow(string rowKey, IEnumerable<string> columnData)
        {
            this.rowData[rowKey] = columnData.ToArray();
        }

        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }
    }
}