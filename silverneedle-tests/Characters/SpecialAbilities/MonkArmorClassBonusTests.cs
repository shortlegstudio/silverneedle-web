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
    using SilverNeedle.Serialization;

    public class MonkArmorClassBonusTests : RequiresDataFiles
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

        [Fact]
        public void ImprovesArmorClassWithLevelUps()
        {
            var monk = CharacterTestTemplates.MarkyMonk();
            var oldAC = monk.Defense.ArmorClass.TotalValue;
            var dataTable = new DataTable("monk abilities");
            dataTable.SetColumns(new string[] {"ac-bonus" } );
            dataTable.AddRow("1", new string[] { "0" });
            dataTable.AddRow("2", new string[] { "1" });
            monk.Add(new MonkArmorClassBonus(dataTable));
            monk.SetLevel(2);
            Assert.Equal(oldAC + 1, monk.Defense.ArmorClass.TotalValue);
        }
    }
}