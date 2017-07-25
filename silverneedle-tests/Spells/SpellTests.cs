// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Spells
{
    using Xunit;
    using SilverNeedle.Spells;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class SpellTests
    {
        [Fact]
        public void LoadingFromObjectStore()
        {
            var data = new MemoryStore();
            data.SetValue("name", "Acid Splash");
            data.SetValue("school", "evocation");
            data.SetValue("subschool", "smash stuff");
            data.SetValue("descriptors", "good, conjuration, acid");

            var spell = new Spell(data);
            Assert.Equal(spell.Name, "Acid Splash");
            Assert.Equal(spell.School, "evocation");
            Assert.Equal(spell.Subschool, "smash stuff");
            Assert.NotStrictEqual(spell.Descriptors, new string[] {"good", "conjuration", "acid" });
        }
    }
}