// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.ClassFeatures
{
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;

    [TestFixture]
    public class ConfigureFastMovementTests
    {
        [Test]
        public void SetsUpAModifierThatIncreasesMovementSpeedWhenNotWearingHeavyArmor()
        {
            var data = new MemoryStore();
            data.SetValue("speed", 10);
            var character = new CharacterSheet();
            var action = new ConfigureFastMovement(data);

            action.Process(character, new CharacterBuildStrategy());
            Assert.That(character.Movement.MovementSpeed, Is.EqualTo(40));
            var heavyArmor = new Armor();
            heavyArmor.ArmorType = ArmorType.Heavy;
            character.Inventory.EquipItem(heavyArmor);
            Assert.That(character.Movement.MovementSpeed, Is.EqualTo(30));

        }
    }
}