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
    public class LevelAbilityTests
    {
        [Test]
        public void LoadsRequiredFieldsFromObjectStore()
        {
            var inMemoryStore = new MemoryStore();
            inMemoryStore.SetValue("type", "Awesome Sauce");
            inMemoryStore.SetValue("condition", "vs. Spells");
            inMemoryStore.SetValue("name", "Food");


            var la = new LevelAbility(inMemoryStore);
            Assert.AreEqual("Food", la.Name);
            Assert.AreEqual("vs. Spells", la.Condition);
            Assert.AreEqual("Awesome Sauce", la.Type);
        }

        [Test]
        public void ConditionIsAnOptionalConstructorValue()
        {
            var inMemoryStore = new MemoryStore();
            inMemoryStore.SetValue("type", "Awesome Sauce");
            inMemoryStore.SetValue("name", "Food");


            var la = new LevelAbility(inMemoryStore);
            Assert.AreEqual("Food", la.Name);
            Assert.AreEqual("", la.Condition);
            Assert.AreEqual("Awesome Sauce", la.Type);
        }
    }
}