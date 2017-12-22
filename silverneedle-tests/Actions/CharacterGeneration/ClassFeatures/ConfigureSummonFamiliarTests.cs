// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.ClassFeatures
{
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Actions.CharacterGeneration.ClassFeatures;
    using SilverNeedle.Bestiary;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    
    public class ConfigureSummonFamiliarTests
    {
        Familiar bat;
        ConfigureSummonFamiliar subject;
        public ConfigureSummonFamiliarTests()
        {
            bat = new Familiar("Bat");
            subject = new ConfigureSummonFamiliar(EntityGateway<Familiar>.LoadWithSingleItem(bat));
        }

        [Fact]
        public void ChoosesAFamiliarForTheCharacter()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            character.InitializeComponents();
            subject.ExecuteStep(character);

            var summon = character.Abilities.First() as SummonFamiliar;
            Assert.Equal(summon.Familiar, bat);
        }

        [Fact]
        public void SummonFamiliarModifiesTheCharacterStats()
        {
            bat.Modifiers.Add(new ValueStatModifier("Perception", 5, "bonus", "Familiar (Bat)"));
            var character = new CharacterSheet(CharacterStrategy.Default());
            character.SkillRanks.AddSkill(new Skill("Perception", AbilityScoreTypes.Wisdom, false));
            var baseValue = character.SkillRanks.GetScore("Perception");
            subject.ExecuteStep(character);
            Assert.Equal(character.SkillRanks.GetScore("Perception"), baseValue + 5);
            
        }
    }
}