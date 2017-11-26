// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class AbyssalArcanaTests
    {
        [Fact]
        public void AddsDRToSummonedCreaturesBasedOnLevel()
        {
            var arcana = new AbyssalArcana();
            var sorcerer = CharacterTestTemplates.Sorcerer();
            sorcerer.SetLevel(6);
            sorcerer.Add(arcana);
            Assert.Equal("summoned creatures gain DR 3/good", arcana.BonusAbility);
            Assert.Equal("Abyssal Arcana (summoned creatures gain DR 3/good)", arcana.Name);
        }
    }
}