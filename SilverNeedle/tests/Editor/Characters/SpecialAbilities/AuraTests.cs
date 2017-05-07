/**
 * Copyright (c) 2017 Trevor Redfern
 * 
 * This software is released under the MIT License.
 * https://opensource.org/licenses/MIT
 */

namespace Tests.Characters.SpecialAbilities
{
    using NUnit.Framework;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    [TestFixture]
    public class AuraTests
    {
        [Test]
        public void AurasHaveAType()
        {
            var configure = new MemoryStore();
            configure.SetValue("type", "Good");
            var aura = new Aura(configure);
            Assert.That(aura.AuraType, Is.EqualTo("Good"));
            Assert.That(aura.Name, Is.EqualTo("Aura of Good"));
        }
    }
}