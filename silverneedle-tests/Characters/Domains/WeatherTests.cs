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

    public class WeatherTests : DomainTestBase<Weather>
    {
        public WeatherTests()
        {
            base.InitializeDomain("weather");
        }

        [Fact]
        public void StormBurst()
        {
            var touch = character.Get<StormBurst>();
            Assert.NotNull(touch); 
            Assert.Equal(touch.UsesPerDay, 6);
        }

        [Fact]
        public void LightningLord()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var aura = character.Get<LightningLord>();
            Assert.NotNull(aura); 
        }
    }
}