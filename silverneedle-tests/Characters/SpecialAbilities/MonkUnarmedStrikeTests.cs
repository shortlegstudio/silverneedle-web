// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    public class MonkUnarmedStrikeTests
    {
        private MonkUnarmedStrike unarmedStrike;
        public MonkUnarmedStrikeTests()
        {

            var configure = new MemoryStore();
            var damageTable = new MemoryStore();
            damageTable.AddListItem(new MemoryStore("1", "1d6"));
            damageTable.AddListItem(new MemoryStore("2", "1d8"));
            damageTable.AddListItem(new MemoryStore("3", "2d10"));
            configure.SetValue("damage-table", damageTable);
            unarmedStrike = new MonkUnarmedStrike(configure);
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
            unarmedStrike.LeveledUp(monk.Components);
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
            unarmedStrike.LeveledUp(monk.Components);
            Assert.Contains(unarmedStrike.Attack, monk.Offense.Attacks());
            Assert.Equal("2d8", unarmedStrike.Attack.Damage.ToString());
        }
    }
}