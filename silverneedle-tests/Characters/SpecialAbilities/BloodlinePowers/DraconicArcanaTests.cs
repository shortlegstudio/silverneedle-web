// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using Moq;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class DraconicArcanaTests : RequiresDataFiles
    {
        [Fact]
        public void AddsDamageForDamageTypeOfDragon()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            var mockDraconic = new Mock<IDraconicBloodline>();
            var dragonType = new DragonType();
            dragonType.EnergyType = "acid";
            mockDraconic.Setup(x => x.DragonType).Returns(dragonType);
            sorcerer.Add(mockDraconic.Object);

            var arcana = new DraconicArcana();
            sorcerer.Add(arcana);
            Assert.Equal("acid spells deal +1 damage per die", arcana.BonusAbility);
        }
    }
}