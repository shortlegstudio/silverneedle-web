// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.ClassFeatures
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    
    public class ConfigureSneakAttackTests
    {
        ConfigureSneakAttack subject;
        public ConfigureSneakAttackTests()
        {
            var memStore = new MemoryStore();
            memStore.SetValue("damage", "2d6");
            subject = new ConfigureSneakAttack(memStore);

        }

        [Fact]
        public void AddsSneakAttackIfNotAlreadyConfigured()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            subject.ExecuteStep(character);
            Assert.NotNull(character.Get<SneakAttack>());
        }

        [Fact]
        public void UpdatesDamageToSneakAttackIfAlreadyConfigured()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            var sneak = new SneakAttack();
            sneak.SetDamage("1d6");
            character.Add(sneak);
            subject.ExecuteStep(character);
            Assert.Equal(sneak.Damage, "2d6");
        }
    }
}