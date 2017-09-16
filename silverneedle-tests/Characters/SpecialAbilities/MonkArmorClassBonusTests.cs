// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Equipment;

    public class MonkArmorClassBonusTests
    {
        [Fact]
        public void AddsWisdomModifierIfPositiveToArmorClassIfNotWearingArmor()
        {
            var monk = CharacterTestTemplates.MarkyMonk();
            monk.AbilityScores.SetScore(AbilityScoreTypes.Wisdom, 18); //+4 modifier
            var oldAC = monk.Defense.ArmorClass.TotalValue;
            monk.Add(new MonkArmorClassBonus());
            Assert.Equal(oldAC + 4, monk.Defense.ArmorClass.TotalValue);

            var anyArmor = new Armor();
            monk.Inventory.EquipItem(anyArmor);
            Assert.Equal(oldAC, monk.Defense.ArmorClass.TotalValue);
            
        }
    }
}