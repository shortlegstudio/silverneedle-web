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

    public class DestructionTests : DomainTestBase<Destruction>
    {
        public DestructionTests()
        {
            base.InitializeDomain("destruction");
        }

        [Fact]
        public void DestructiveSmite()
        {
            var touch = character.Get<DestructiveSmite>();
            Assert.NotNull(touch); 
            Assert.Equal(touch.UsesPerDay, 6);
        }

        [Fact]
        public void DestructiveAura()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var destAura = character.Get<DestructiveAura>();
            Assert.NotNull(destAura);
        }
    }
}