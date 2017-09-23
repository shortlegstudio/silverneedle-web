// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    public class MonkUnarmedStrikeTests
    {
        private MonkUnarmedStrike unarmedStrike;
        public MonkUnarmedStrikeTests()
        {
            var dataTable = new DataTable("Monk Abilities");
            dataTable.SetColumns(new string[] { "unarmed-damage" });
            dataTable.AddRow("1", new string[] { "1d6" });
            dataTable.AddRow("2", new string[] { "1d8" });
            dataTable.AddRow("3", new string[] { "2d10" });
            unarmedStrike = new MonkUnarmedStrike(dataTable);
        }

        [Fact]
        public void AddsUnarmedMeleeAttackToOffenseStats()
        {
            var monk = CharacterTestTemplates.MarkyMonk();
            monk.Add(unarmedStrike);
            Assert.Contains(unarmedStrike.Attack, monk.Offense.Attacks());
            Assert.Equal("1d6", unarmedStrike.Attack.Damage.ToString());
        }

        [Fact]
        public void AdjustDamageToMeetCharacterSize()
        {
            var monk = CharacterTestTemplates.MarkyMonk();
            monk.Size.SetSize(CharacterSize.Small, 0, 0);
            monk.Add(unarmedStrike);
            Assert.Equal("1d4", unarmedStrike.Attack.Damage.ToString());

        }

        [Fact]
        public void DamageIncreasesWithCharacterLevel()
        {
            var monk = CharacterTestTemplates.MarkyMonk();
            monk.Add(unarmedStrike);
            monk.SetLevel(2);
            Assert.Contains(unarmedStrike.Attack, monk.Offense.Attacks());
            Assert.Equal("1d8", unarmedStrike.Attack.Damage.ToString());
        }

        [Fact]
        public void DamageIncreasesWithCharacterLevelAccountForCharacterSize()
        {
            var monk = CharacterTestTemplates.MarkyMonk();
            monk.Size.SetSize(CharacterSize.Small, 0, 0);
            monk.Add(unarmedStrike);
            monk.SetLevel(3);
            Assert.Contains(unarmedStrike.Attack, monk.Offense.Attacks());
            Assert.Equal("2d8", unarmedStrike.Attack.Damage.ToString());
        }
    }
}