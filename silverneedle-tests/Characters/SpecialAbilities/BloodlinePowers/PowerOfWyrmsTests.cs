// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using Moq;
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;
    using SilverNeedle.Characters.Senses;
    public class PowerOfWyrmsTests
    {
        private CharacterSheet sorcerer;
        private PowerOfWyrms power;

        public PowerOfWyrmsTests()
        {
            sorcerer = CharacterTestTemplates.Sorcerer();
            power = new PowerOfWyrms();
            var draconicBloodline = new Mock<IDraconicBloodline>();
            var dragonType = new DragonType();
            dragonType.EnergyType = "electricity";
            draconicBloodline.Setup(x => x.DragonType).Returns(dragonType);

            sorcerer.Add(draconicBloodline.Object);
            sorcerer.Add(power);

        }

        [Fact]
        public void GrantsImmunities()
        {
            AssertCharacter.IsImmuneTo("electricity", sorcerer);
            AssertCharacter.IsImmuneTo("paralysis", sorcerer);
            AssertCharacter.IsImmuneTo("sleep", sorcerer);
        }

        [Fact]
        public void GrantsBlindsenseSixtyFeet()
        {
            Assert.True(sorcerer.Contains<Blindsense>());
        }
    }
}