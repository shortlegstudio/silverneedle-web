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

    public class SunTests : DomainTestBase<Sun>
    {
        public SunTests()
        {
            base.InitializeDomain("sun");
        }

        [Fact]
        public void SunBlessing()
        {
            var touch = character.Get<SunBlessing>();
            Assert.NotNull(touch); 
        }

        [Fact]
        public void NimbusOfLight()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var aura = character.Get<NimbusOfLight>();
            Assert.NotNull(aura);
        }
    }
}