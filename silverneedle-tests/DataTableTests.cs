// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests
{
    using System.Collections.Generic;
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class DataTableTests
    {
        [Fact]
        public void DataTablesHaveRowsOfData()
        {
            var yamlConfigure = @"
name: table
columns: [damage]
table:
  1: [ 1d6 ]
  2: [ 1d8 ]";
            var configure = yamlConfigure.ParseYaml();
            var table = new DataTable(configure);
            Assert.Equal("1d6", table.Get("1","damage"));
            Assert.Equal("table", table.Name);
        }


    }
}