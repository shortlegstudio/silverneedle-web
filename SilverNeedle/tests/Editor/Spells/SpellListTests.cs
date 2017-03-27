// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Spells
{
    using NUnit.Framework;
    using SilverNeedle.Spells;
    using SilverNeedle.Utility;

    [TestFixture]
    public class SpellListTests
    {
        [Test]
        public void SpellListsHaveSpellsForEachLevel()
        {
            var data = new MemoryStore();
            data.SetValue("class", "foo");
            var levels = new MemoryStore();
            levels.SetValue("0", "read stuff, do stuff");
            levels.SetValue("1", "kill, explosion");
            data.SetValue("levels", levels);
            var spellList = new SpellList(data);

            Assert.That(spellList.Class, Is.EqualTo("foo"));
            Assert.That(spellList.Levels.Count, Is.EqualTo(2));
            Assert.That(spellList.Levels[0], Is.EquivalentTo(new string[] {"read stuff", "do stuff"}));
            Assert.That(spellList.Levels[1], Is.EquivalentTo(new string[] {"kill", "explosion"}));
            Assert.That(spellList.Matches("FOO"), Is.True);
        }
    }
}