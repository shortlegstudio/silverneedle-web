// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Bestiary
{
    using Xunit;
    using SilverNeedle.Bestiary;
    using SilverNeedle.Serialization;
    
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
            Assert.Equal("Bat", familiar.Name);
            Assert.Equal(1, familiar.Modifiers.Count);
            Assert.Equal("Perception", familiar.Modifiers[0].StatisticName);
            Assert.Equal("bonus", familiar.Modifiers[0].Type);
            Assert.Equal(2, familiar.Modifiers[0].Modifier);
        }
    }
}