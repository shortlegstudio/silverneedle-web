// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace Tests.Characters 
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;

    
    public class ArmorTrainingTests 
    {
        [Fact]
        public void RegistersModifiersWithStats()
        {
            var character = CharacterTestTemplates.AverageBob().WithSkills(new string[] { "Test" });
            var defStats = character.Get<DefenseStats>();
            var abilityScores = character.Get<AbilityScores>();
            var skillRanks = character.Get<SkillRanks>();
            var moveStats = character.Get<MovementStats>();
            var armorTrain = new ArmorTraining();
            armorTrain.SetLevel(2);
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
            armorTrain.SetLevel(3);
            Assert.Equal(armorTrain.MaxDexBonusModifier.Modifier, 3);
            Assert.Equal(armorTrain.ArmorCheckBonusModifier.Modifier, 3);
        }
    }
}
