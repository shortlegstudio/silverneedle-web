// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Senses;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;
    using Xunit;

    public class AberrantFormTests
    {
        [Fact]
        public void GrantsImmunityToCriticalsAndSneakAttacks()
        {
            var sorc = CharacterTestTemplates.Sorcerer();
            var aberrant = new AberrantForm();
            sorc.Add(aberrant);
            AssertCharacter.IsImmuneTo("Criticals", sorc);
            AssertCharacter.IsImmuneTo("Sneak Attacks", sorc);
        }

        [Fact]
        public void GrantsDamageResistanceFive()
        {
            var sorc = CharacterTestTemplates.Sorcerer();
            var aberrant = new AberrantForm();
            sorc.Add(aberrant);
            var dr = sorc.Defense.EnergyResistance.First();
            Assert.Equal(5, dr.Amount);
            Assert.Equal("-", dr.DamageType);
        }

        [Fact]
        public void GrantsBlindSenseSixtyFeet()
        {
            var sorc = CharacterTestTemplates.Sorcerer();
            var aberrant = new AberrantForm();
            sorc.Add(aberrant);
            var sense = sorc.GetAll<ISense>().First(x => x is Blindsight);
            Assert.Equal("Blindsight 50ft", sense.DisplayString());
        }
    }
}