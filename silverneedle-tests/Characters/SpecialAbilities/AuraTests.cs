/**
 * Copyright (c) 2017 Trevor Redfern
 * 
 * This software is released under the MIT License.
 * https://opensource.org/licenses/MIT
 */

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    
    public class AuraTests
    {
        [Fact]
        public void AurasHaveAType()
        {
            var configure = new MemoryStore();
            configure.SetValue("type", "Good");
            var aura = new Aura(configure);
            Assert.Equal(aura.AuraType, "Good");
            Assert.Equal(aura.Name, "Aura of Good");
        }
    }
}