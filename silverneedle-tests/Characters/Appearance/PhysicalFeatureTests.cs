// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Appearance
{
    using Xunit;
    using SilverNeedle.Characters.Appearance;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    
    public class PhysicalFeatureTests
    {
        [Fact]
        public void HasADefaultTemplate()
        {
            var store = new MemoryStore();
            store.SetValue("name", "tattoo");
            var feature = new PhysicalFeature(store);
            Assert.Equal("{{pronoun}} has a {{description}}.", feature.Templates[0].Template);
        }
    }
}