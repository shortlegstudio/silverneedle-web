
namespace Tests.Characters 
{
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;

    [TestFixture]
    public class ArmorTrainingTests 
    {
        MemoryStore baseValues;

        [SetUp]
        public void SetUp()
        {
            baseValues = new MemoryStore();
            baseValues.SetValue("name", "Armor Training");
            baseValues.SetValue("type", "ArmorTraining");            
        }

        [Test]
        public void ArmorTrainingImprovesMaximumDexterityBonus()
        {
            baseValues.SetValue("level", 2);
            var training = new ArmorTraining(baseValues);
            var heavyArmor = new Armor();
            heavyArmor.MaximumDexterityBonus = 1;
            Assert.AreEqual(3, training.GetMaximumDexterityBonus(heavyArmor));
        }

        [Test]
        public void AtLevelOneAllowsMaximumMovementSpeedInMediumArmor()
        {
            baseValues.SetValue("level", 1);
            var training = new ArmorTraining(baseValues);
            var medArmor = new Armor();
            medArmor.ArmorType = ArmorType.Medium;
            var heavyArmor = new Armor();
            heavyArmor.ArmorType = ArmorType.Heavy;

            Assert.AreEqual(25, training.GetMovementSpeed(25, medArmor));
            Assert.AreEqual(15, training.GetMovementSpeed(25, heavyArmor));
        }

        [Test]
        public void AtLevelTwoAllowsMaximumMovementSpeedInHeavyArmorAndMediumArmor()
        {
            baseValues.SetValue("level", 2);
            
            var training = new ArmorTraining(baseValues);
            var medArmor = new Armor();
            medArmor.ArmorType = ArmorType.Medium;
            var heavyArmor = new Armor();
            heavyArmor.ArmorType = ArmorType.Heavy;

            Assert.AreEqual(25, training.GetMovementSpeed(25, medArmor));
            Assert.AreEqual(25, training.GetMovementSpeed(25, heavyArmor));            
        }

        [Test]
        public void ReducesArmorCheckPenaltyOnePointEveryLevel()
        {
            baseValues.SetValue("level", 3);
            
            var train = new ArmorTraining(baseValues);
            var armor = new Armor();
            armor.ArmorCheckPenalty = 5;
            Assert.AreEqual(2, train.GetArmorCheckPenalty(armor));
        }

        [Test]
        public void ArmorCheckPenaltyNeverGoesBelowZero()
        {
            baseValues.SetValue("level", 3);
            
            var train = new ArmorTraining(baseValues);
            var armor = new Armor();
            Assert.AreEqual(0, train.GetArmorCheckPenalty(armor));
        }
    }
}
