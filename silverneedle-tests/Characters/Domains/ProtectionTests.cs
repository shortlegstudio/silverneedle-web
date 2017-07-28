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

    public class ProtectionTests : DomainTestBase<Protection>
    {
        public ProtectionTests()
        {
            base.InitializeDomain("protection");
        }

        [Fact]
        public void ResistantTouch()
        {
            var touch = character.Get<ResistantTouch>();
            Assert.NotNull(touch); 
            Assert.Equal(touch.UsesPerDay, 6);
        }

        [Fact]
        public void AuraOfProtection()
        {
            character.SetLevel(8);
            domain.LeveledUp(character.Components);
            var aura = character.Get<AuraOfProtection>();
            Assert.NotNull(aura); 
        }
    }
}