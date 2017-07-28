// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters {
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    
    public class IdealTests 
    {
        [Fact]
        public void ParseFromObjectStore()
        {
            var store = new MemoryStore();
            store.SetValue("name", "Faith");
            store.SetValue("description", "Trust in my deity.");

            var ideal = new Ideal(store);
            Assert.Equal("Faith", ideal.Name);
            Assert.Equal("Trust in my deity.", ideal.Description);
        }
    }
}