// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    [TestFixture]
    public class CharacterWealthTests
    {
        [Fact]
        public void LoadsWealthFromTable()
        {
            var data = new MemoryStore();
            data.SetValue("name", "foo");
            var levels = new MemoryStore();
            var level1 = new MemoryStore();
            level1.SetValue("level", 1);
            level1.SetValue("amount", "0gp");
            
            var level2 = new MemoryStore();
            level2.SetValue("level", 2);
            level2.SetValue("amount", "220gp");
            levels.AddListItem(level1);
            levels.AddListItem(level2);
            data.SetValue("levels", levels);
            
            var wealth = new CharacterWealth(data);
            Assert.That(wealth.Name, Is.EqualTo("foo"));
            Assert.That(wealth.Levels[0].Level, Is.EqualTo(1));
            Assert.That(wealth.Levels[0].Value, Is.EqualTo(0));
            Assert.That(wealth.Levels[1].Level, Is.EqualTo(2));
            Assert.That(wealth.Levels[1].Value, Is.EqualTo(22000));

        }
    } 
}