// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class CelestialArcanaTests
    {
        [Fact]
        public void AddsDRToSummonedCreaturesBasedOnLevel()
        {
            var arcana = new CelestialArcana();
            var sorcerer = CharacterTestTemplates.Sorcerer();
            sorcerer.SetLevel(6);
            sorcerer.Add(arcana);
            Assert.Equal("summoned creatures gain DR 3/evil", arcana.BonusAbility);
        }
    }
}