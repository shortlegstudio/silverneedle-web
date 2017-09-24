// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    public class DiamondSoulTests
    {
        [Fact]
        public void AddsSpellResistanceTenPlusLevel()
        {
            var monk = CharacterTestTemplates.MarkyMonk();
            var soul = new DiamondSoul();
            monk.Add(soul);
            monk.SetLevel(6);

            var defense = monk.Get<DefenseStats>();
            Assert.Equal(16, defense.SpellResistance.TotalValue);
        }
    }
}