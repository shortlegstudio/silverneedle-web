// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    
    public class WeaponTrainingTests
    {
            string yaml = @"---
name: Weapon Training
base-value: 1";
        [Fact]
        public void RegisterWeaponModifiersWithOffenseStats()
        {
            var character = CharacterTestTemplates.AverageBob();
            var weaponTraining = new WeaponTraining(yaml.ParseYaml());
            character.Add(weaponTraining);
            Assert.Equal(1, weaponTraining.WeaponAttackBonus.Modifier);
            Assert.Equal(1, weaponTraining.WeaponDamageBonus.Modifier);
            Assert.Equal(1, weaponTraining.TotalValue);
            Assert.Equal(string.Format("Weapon Training ({0} +1)", weaponTraining.Group), weaponTraining.DisplayString());
        }

        [Fact]
        public void SelectsAUniqueWeaponGroupWhenAddedToCharacter()
        {
            var weaponTraining1 = new WeaponTraining(yaml.ParseYaml());
            var weaponTraining2 = new WeaponTraining(yaml.ParseYaml());
            var character = CharacterTestTemplates.AverageBob();
            character.Add(weaponTraining1);
            character.Add(weaponTraining2);
            Assert.NotEqual(weaponTraining1.Group, weaponTraining2.Group);
        }

        [Fact]
        public void AddingAModifierWillIncreaseThePowerOfWeaponTraining()
        {
            var wt = new WeaponTraining(yaml.ParseYaml());
            var character = CharacterTestTemplates.AverageBob();
            character.Add(wt);
            var modYaml = @"---
name: Weapon Training
modifier: 1
modifier-type: level-up";
            var modifier = new ValueStatModifier(modYaml.ParseYaml());
            character.Add(modifier);
            Assert.Equal(2, wt.TotalValue);
            Assert.Equal(string.Format("Weapon Training ({0} +2)", wt.Group), wt.DisplayString());
        }
    }
}