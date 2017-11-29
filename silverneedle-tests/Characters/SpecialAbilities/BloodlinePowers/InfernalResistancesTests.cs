// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class InfernalResistancesTests
    {
        private CharacterSheet sorcerer;
        private InfernalResistances resistances;
        public InfernalResistancesTests()
        {
            sorcerer = CharacterTestTemplates.Sorcerer();
            resistances = new InfernalResistances();
            sorcerer.Add(resistances);
        }

        [Fact]
        public void GrantsResistances()
        {
            AssertCharacter.HasDamageResistance(sorcerer, "fire", 5);
            sorcerer.SetLevel(9);
            resistances.LeveledUp(sorcerer.Components);
        }

        [Fact]
        public void GrantsSaveBonuses()
        {
            Assert.Equal(2, sorcerer.Defense.FortitudeSave.GetConditionalValue("poison"));
            Assert.Equal(2, sorcerer.Defense.ReflexSave.GetConditionalValue("poison"));
            Assert.Equal(2, sorcerer.Defense.WillSave.GetConditionalValue("poison"));
        }
    }
}