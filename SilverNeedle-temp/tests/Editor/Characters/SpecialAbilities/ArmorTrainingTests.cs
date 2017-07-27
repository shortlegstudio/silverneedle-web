
namespace Tests.Characters 
{
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;

    [TestFixture]
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
            Assert.That(maxDex.Modifiers, Contains.Item(armorTrain.MaxDexBonusModifier));

            // Armor Check Penalty
            var armorCheck = skillRanks.ArmorCheckPenalty;
            Assert.That(armorCheck.Modifiers, Contains.Item(armorTrain.ArmorCheckBonusModifier));

            // Armor Movement Penalty
            var armorMove = moveStats.ArmorMovementPenalty;
            Assert.That(armorMove.Modifiers, Contains.Item(armorTrain.ArmorMovementBonusModifier));

            //Increase ArmorTraining Level Improves Modifiers
            armorTrain.SetLevel(3);
            Assert.That(armorTrain.MaxDexBonusModifier.Modifier, Is.EqualTo(3));
            Assert.That(armorTrain.ArmorCheckBonusModifier.Modifier, Is.EqualTo(3));
        }
    }
}
