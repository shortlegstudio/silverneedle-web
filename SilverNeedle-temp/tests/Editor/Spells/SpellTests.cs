// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Spells
{
    using NUnit.Framework;
    using SilverNeedle.Spells;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    [TestFixture]
    public class SpellTests
    {
        [Test]
        public void LoadingFromObjectStore()
        {
            var data = new MemoryStore();
            data.SetValue("name", "Acid Splash");
            data.SetValue("school", "evocation");
            data.SetValue("subschool", "smash stuff");
            data.SetValue("descriptors", "good, conjuration, acid");

            var spell = new Spell(data);
            Assert.That(spell.Name, Is.EqualTo("Acid Splash"));
            Assert.That(spell.School, Is.EqualTo("evocation"));
            Assert.That(spell.Subschool, Is.EqualTo("smash stuff"));
            Assert.That(spell.Descriptors, Is.EquivalentTo(new string[] {"good", "conjuration", "acid" }));
        }
    }
}