// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Domains
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Domains;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    
    public class AirDomainTests
    {
        private Air airDomain;
        private CharacterSheet character;

        public AirDomainTests()
        {
            var data = new MemoryStore();
            data.SetValue("spells", "");
            data.SetValue("name", "air");
            airDomain = new Air(data);

            character = new CharacterSheet(CharacterStrategy.Default());
            character.InitializeComponents();
            var Class = new Class("cleric");
            character.SetClass(Class);
            character.AbilityScores.SetScore(AbilityScoreTypes.Wisdom, 16);
            character.Add(airDomain);
        }

        [Fact]
        public void AddFirstLevelGrantsElecticityAttack()
        {
            var lightning = character.Offense.Attacks().OfType<Air.LightningArcAttack>().First();
            Assert.Equal(lightning.UsesPerDay, 6);
            Assert.Equal(lightning.Damage.ToString(), "1d6+1");
            Assert.Equal(lightning.ToString(), "Lightning Arc 30' (1d6+1 electricity)");
        }

        [Fact]
        public void AddsElectricityDamageResistance()
        {
            // Level Up Notification
            character.SetLevel(6);
            airDomain.LeveledUp(character.Components);

            var elec = character.Defense.DamageResistance.First();
            Assert.Equal(elec.DamageType, "electricity");
            Assert.Equal(elec.Amount, 10);

            character.SetLevel(12);
            airDomain.LeveledUp(character.Components);
            Assert.Equal(elec.Amount, 20);

            //At level 20 remove DR and add immunity
            character.SetLevel(20);
            airDomain.LeveledUp(character.Components);

            Assert.Equal(elec.Amount, 0);
            var immune = character.Defense.Immunities.First();
            Assert.Equal(immune.Condition, "electricity");

        }
    }
}