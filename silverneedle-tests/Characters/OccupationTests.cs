// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    
    public class OccupationTests
    {
        [Fact]
        public void CanParseFromObjectStore()
        {
            var data = new MemoryStore();
            data.SetValue("name", "Pig Farmer");
            data.SetValue("class", "commoner");

            var oc = new Occupation(data);
            Assert.Equal("Pig Farmer", oc.Name);
            Assert.Equal("commoner", oc.Class);
            
        }
    }
}