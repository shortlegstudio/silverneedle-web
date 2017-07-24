// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Domains
{
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Domains;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    [TestFixture]
    public class AirDomainTests
    {
        private Air airDomain;
        private CharacterSheet character;

        [SetUp]
        public void Configure()
        {
            var data = new MemoryStore();
            data.SetValue("spells", "");
            data.SetValue("name", "air");
            airDomain = new Air(data);

            character = new CharacterSheet();
            character.InitializeComponents();
            var Class = new Class("cleric");
            character.SetClass(Class);
            character.AbilityScores.SetScore(AbilityScoreTypes.Wisdom, 16);
            character.Add(airDomain);
        }

        [Test]
        public void AddFirstLevelGrantsElecticityAttack()
        {
            var lightning = character.Offense.Attacks().OfType<Air.LightningArcAttack>().First();
            Assert.That(lightning.UsesPerDay, Is.EqualTo(6));
            Assert.That(lightning.Damage.ToString(), Is.EqualTo("1d6+1"));
            Assert.That(lightning.ToString(), Is.EqualTo("Lightning Arc 30' (1d6+1 electricity)"));
        }

        [Test]
        public void AddsElectricityDamageResistance()
        {
            // Level Up Notification
            character.SetLevel(6);
            airDomain.LeveledUp(character.Components);

            var elec = character.Defense.DamageResistance.First();
            Assert.That(elec.DamageType, Is.EqualTo("electricity"));
            Assert.That(elec.Amount, Is.EqualTo(10));

            character.SetLevel(12);
            airDomain.LeveledUp(character.Components);
            Assert.That(elec.Amount, Is.EqualTo(20));

            //At level 20 remove DR and add immunity
            character.SetLevel(20);
            airDomain.LeveledUp(character.Components);

            Assert.That(elec.Amount, Is.EqualTo(0));
            var immune = character.Defense.Immunities.First();
            Assert.That(immune.Condition, Is.EqualTo("electricity"));

        }


    }
}