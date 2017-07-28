// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Domains
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Domains;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    public class MadnessTests : DomainTestBase<Madness>
    {
        public MadnessTests()
        {
            base.InitializeDomain("madness");
        }

        [Fact]
        public void VisionOfMadness()
        {
            var touch = character.Get<VisionOfMadness>();
            Assert.NotNull(touch);
            Assert.Equal(touch.UsesPerDay, 6);
        }

        [Fact]
        public void AuraOfMadness()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var aura = character.Get<AuraOfMadness>();
            Assert.NotNull(aura); 
        }
    }
}