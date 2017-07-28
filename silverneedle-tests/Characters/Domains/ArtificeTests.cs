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

    public class ArtificeTests
    {
        private Artifice artificeDomain;
        private CharacterSheet character;

        public ArtificeTests()
        {
            var data = new MemoryStore();
            data.SetValue("spells", "");
            data.SetValue("name", "artifice");
            artificeDomain = new Artifice(data);

            character = new CharacterSheet();
            character.InitializeComponents();
            var Class = new Class("cleric");
            character.SetClass(Class);
            character.AbilityScores.SetScore(AbilityScoreTypes.Wisdom, 16);
            character.Add(artificeDomain);
        }

        [Fact]
        public void CanMendItems()
        {
            var touch = character.Get<ArtificerTouch>();
            Assert.NotNull(touch);
            Assert.Equal(touch.UsesPerDay, 6);
        }

        [Fact]
        public void CanMakeWeaponsDance()
        {
            character.SetLevel(8);
            artificeDomain.LeveledUp(character.Components);
            var danceWeapon = character.Get<DancingWeapons>();
            Assert.NotNull(danceWeapon);
            Assert.Equal(danceWeapon.UsesPerDay, 1);
            character.SetLevel(16);

            Assert.Equal(danceWeapon.UsesPerDay, 3);
        }
    }
}