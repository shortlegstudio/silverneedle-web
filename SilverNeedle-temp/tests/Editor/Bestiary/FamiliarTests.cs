// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Bestiary
{
    using NUnit.Framework;
    using SilverNeedle.Bestiary;
    using SilverNeedle.Serialization;
    
    [TestFixture]
    public class FamiliarTests
    {
        [Fact]
        public void LoadFromObjectStore()
        {
            var data = new MemoryStore();
            data.SetValue("name", "Bat");
            var modifiers = new MemoryStore();
            var statMod = new MemoryStore();
            statMod.SetValue("stat", "Perception");
            statMod.SetValue("type", "bonus");
            statMod.SetValue("modifier", 2);
            statMod.SetValue("condition", "darkness");
            modifiers.AddListItem(statMod);
            data.SetValue("modifiers", modifiers);

            var familiar = new Familiar(data);
            Assert.That(familiar.Name, Is.EqualTo("Bat"));
            Assert.That(familiar.Modifiers.Count, Is.EqualTo(1));
            Assert.That(familiar.Modifiers[0].StatisticName, Is.EqualTo("Perception"));
            Assert.That(familiar.Modifiers[0].Type, Is.EqualTo("bonus"));
            Assert.That(familiar.Modifiers[0].Modifier, Is.EqualTo(2));
        }
    }
}