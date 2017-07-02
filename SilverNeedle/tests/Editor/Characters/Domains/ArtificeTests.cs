// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Domains
{
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Domains;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    public class ArtificeTests
    {
        private Artifice artificeDomain;
        private CharacterSheet character;

        [SetUp]
        public void Configure()
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

        [Test]
        public void CanMendItems()
        {
            var touch = character.Get<ArtificerTouch>();
            Assert.That(touch, Is.Not.Null); 
            Assert.That(touch.UsesPerDay, Is.EqualTo(6));
        }

        [Test]
        public void CanMakeWeaponsDance()
        {
            character.SetLevel(8);
            artificeDomain.LeveledUp(character.Components);
            var danceWeapon = character.Get<DancingWeapons>();
            Assert.That(danceWeapon, Is.Not.Null); 
            Assert.That(danceWeapon.UsesPerDay, Is.EqualTo(1));
            character.SetLevel(16);

            Assert.That(danceWeapon.UsesPerDay, Is.EqualTo(3));
        }
    }
}