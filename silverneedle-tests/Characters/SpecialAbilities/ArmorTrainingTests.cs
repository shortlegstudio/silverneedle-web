// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace Tests.Characters 
{
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    
    public class ArmorTrainingTests 
    {
        [Fact]
        public void RegistersModifiersWithStats()
        {
            var character = CharacterTestTemplates.AverageBob();
            var defStats = character.Get<DefenseStats>();
            var abilityScores = character.Get<AbilityScores>();
            var skillRanks = character.Get<SkillRanks>();
            var moveStats = character.Get<MovementStats>();
            var yaml = @"---
name: Armor Training
base-value: 1";
            var armorTrain = new ArmorTraining(yaml.ParseYaml());
            armorTrain.AddModifier(new ValueStatModifier(1));
            //Set to second level
            character.Add(armorTrain);

            // Max Dex Bonus
            var maxDex = defStats.MaxDexterityBonus;
            Assert.Contains(armorTrain.MaxDexBonusModifier, maxDex.Modifiers);

            // Armor Check Penalty
            var armorCheck = skillRanks.ArmorCheckPenalty;
            Assert.Contains(armorTrain.ArmorCheckBonusModifier, armorCheck.Modifiers);

            // Armor Movement Penalty
            var armorMove = moveStats.ArmorMovementPenalty;
            Assert.Contains(armorTrain.ArmorMovementBonusModifier, armorMove.Modifiers);

            //Increase ArmorTraining Level Improves Modifiers
            armorTrain.AddModifier(new ValueStatModifier(1));
            Assert.Equal(3, armorTrain.MaxDexBonusModifier.Modifier);
            Assert.Equal(3, armorTrain.ArmorCheckBonusModifier.Modifier);
        }
    }
}
