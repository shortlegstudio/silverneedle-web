// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.ClassFeatures
{
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    [TestFixture]
    public class ConfigureSneakAttackTests
    {
        ConfigureSneakAttack subject;
        [SetUp]
        public void Configure()
        {
            var memStore = new MemoryStore();
            memStore.SetValue("damage", "2d6");
            subject = new ConfigureSneakAttack(memStore);

        }

        [Test]
        public void AddsSneakAttackIfNotAlreadyConfigured()
        {
            var character = new CharacterSheet();
            subject.Process(character, new CharacterBuildStrategy());
            Assert.That(character.Get<SneakAttack>(), Is.Not.Null);
        }

        [Test]
        public void UpdatesDamageToSneakAttackIfAlreadyConfigured()
        {
            var character = new CharacterSheet();
            var sneak = new SneakAttack();
            sneak.SetDamage("1d6");
            character.Add(sneak);
            subject.Process(character, new CharacterBuildStrategy());
            Assert.That(sneak.Damage, Is.EqualTo("2d6"));
        }
    }
}