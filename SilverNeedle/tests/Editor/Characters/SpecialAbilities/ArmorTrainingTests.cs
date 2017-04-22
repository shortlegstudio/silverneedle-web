
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
        [Test]
        public void RegistersModifiersWithStats()
        {
            var defStats = new DefenseStats();
            var armorTrain = new ArmorTraining();
            armorTrain.Level = 2;
            var bag = new ComponentBag();
            bag.Add(defStats);
            bag.Add(armorTrain);

            armorTrain.Initialize(bag);
            var maxDex = defStats.MaxDexterityBonus;
            Assert.That(maxDex.Modifiers, Contains.Item(armorTrain.MaxDexBonusModifier));

            //Increase ArmorTraining Level Improves Modifiers
            armorTrain.Level = 3;
            Assert.That(armorTrain.MaxDexBonusModifier.Modifier, Is.EqualTo(3));
        }
    }
}
