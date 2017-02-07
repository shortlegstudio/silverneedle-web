using NUnit.Framework;
using SilverNeedle.Characters;
using SilverNeedle.Equipment;


namespace Characters {
    [TestFixture]
    public class ArmorTrainingTests 
    {
        [Test]
        public void ArmorTrainingImprovesMaximumDexterityBonus()
        {
            var training = new ArmorTraining(2);
            var heavyArmor = new Armor();
            heavyArmor.MaximumDexterityBonus = 1;
            Assert.AreEqual(3, training.GetMaximumDexterityBonus(heavyArmor));
        }

        [Test]
        public void AtLevelOneAllowsMaximumMovementSpeedInMediumArmor()
        {
            var training = new ArmorTraining(1);
            var medArmor = new Armor();
            medArmor.ArmorType = ArmorType.Medium;
            var heavyArmor = new Armor();
            heavyArmor.ArmorType = ArmorType.Heavy;

            Assert.AreEqual(25, training.GetMovementSpeed(25, medArmor));
            Assert.AreEqual(15, training.GetMovementSpeed(25, heavyArmor));
        }
    }
}
