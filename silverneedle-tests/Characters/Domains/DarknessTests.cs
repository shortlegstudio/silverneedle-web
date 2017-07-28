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
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    public class DarknessTests : DomainTestBase<Darkness>
    {
        public DarknessTests()
        {
            base.InitializeDomain("darkness");
        }

        [Fact]
        public void TouchOfDarkness()
        {
            var touch = character.Get<TouchOfDarkness>();
            Assert.NotNull(touch);
            Assert.Equal(touch.UsesPerDay, 6);
        }

        [Fact]
        public void EyesOfDarkness()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var eyeOfDark = character.Get<EyesOfDarkness>();
            Assert.NotNull(eyeOfDark);
        }

        [Fact]
        public void ReceiveBlindFightAsBonusFeat()
        {
            var blindFight = character.FeatTokens.First();
            Assert.NotStrictEqual(blindFight.Tags, new string[] { "Blind-Fight" }); 
        }
    }
}