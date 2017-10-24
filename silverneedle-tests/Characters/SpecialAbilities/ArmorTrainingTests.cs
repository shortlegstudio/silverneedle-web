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
            var defStats = new DefenseStats();
            var abilityScores = new AbilityScores();
            var skillRanks = new SkillRanks(new Skill[] { new Skill("Test", AbilityScoreTypes.Strength, false)},abilityScores);
            var armorTrain = new ArmorTraining();
            var moveStats = new MovementStats();
            armorTrain.SetLevel(2);
            var bag = new ComponentBag();
            bag.Add(defStats);
            bag.Add(armorTrain);
            bag.Add(abilityScores);
            bag.Add(skillRanks);
            bag.Add(moveStats);

            armorTrain.Initialize(bag);

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
