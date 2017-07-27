// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Equipment 
{
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;
    using SilverNeedle.Serialization;

    public class ArmorTests {
        [Fact]
        public void DefaultArmorTypeIsNone() {
            var armor = new Armor ();
            Assert.Equal (ArmorType.None, armor.ArmorType);
        }

        [Fact]
        public void ReductionInSpeedForNoArmorIsZero()
        {
            var armor = new Armor();
            armor.ArmorType = ArmorType.None;
            Assert.Equal(0, armor.MovementSpeedPenalty30);            
        }

        [Fact]
        public void ReductionInSpeedForLightArmorIsZero()
        {
            var armor = new Armor();
            armor.ArmorType = ArmorType.Light;
            Assert.Equal(0, armor.MovementSpeedPenalty30);
        }

        [Fact]
        public void ReductionInSpeedForMediumArmorIsTen()
        {
            var armor = new Armor();
            armor.ArmorType = ArmorType.Medium;
            Assert.Equal(-10, armor.MovementSpeedPenalty30);            
            Assert.Equal(-5, armor.MovementSpeedPenalty20);
        }

        [Fact]
        public void ReductionInSpeedForHeavyArmorIsTen()
        {
            var armor = new Armor();
            armor.ArmorType = ArmorType.Heavy;
            Assert.Equal(-10, armor.MovementSpeedPenalty30);            
            Assert.Equal(-5, armor.MovementSpeedPenalty20);
        }

        EntityGateway<Armor> gateway;

        public ArmorTests() 
        {
            gateway = new EntityGateway<Armor> (ArmorYamlFile.ParseYaml ().Load<Armor>());
        }
            
        [Fact]
        public void YouCanGetAllTheArmors() 
        {
            var armors = gateway.All ();
            Assert.Equal (3, armors.Count ());
        }

        [Fact]
        public void YouCanAccessASpecificSetOfArmor() 
        {
            var leather = gateway.Find("Leather Armor");
            Assert.NotNull (leather);
            Assert.Equal ("Leather Armor", leather.Name);

            var plate = gateway.Find("Full Plate");
            Assert.NotNull (plate);
            Assert.Equal ("Full Plate", plate.Name);
        }

        [Fact]
        public void ArmorLoadsItsArmorClass() 
        {
            var leather = gateway.Find("Leather Armor");
            Assert.Equal (2, leather.ArmorClass);
            var plate = gateway.Find ("Full Plate");
            Assert.Equal (9, plate.ArmorClass);
        }

        [Fact]
        public void ArmorHasWeight() 
        {
            var leather = gateway.Find ("Leather Armor");
            Assert.Equal (15, leather.Weight);
        }

        [Fact]
        public void ArmorHasMaxDexBonus() 
        {
            var plate = gateway.Find ("Full Plate");
            Assert.Equal (1, plate.MaximumDexterityBonus);
        }	

        [Fact]
        public void ArmorHasArcaneSpellFailure() 
        {
            var leather = gateway.Find ("Leather Armor");
            Assert.Equal (10, leather.ArcaneSpellFailureChance);
        }

        [Fact]
        public void ArmorHasACheckPenalty() 
        {
            var plate = gateway.Find ("Full Plate");
            Assert.Equal (-6, plate.ArmorCheckPenalty);
        }

        [Fact]
        public void ArmorHasAType() 
        {
            var plate = gateway.Find ("Full Plate");
            Assert.Equal (ArmorType.Heavy, plate.ArmorType);
        }

        [Fact]
        public void GetAllArmorsOfAType() 
        {
            var armors = gateway.FindByArmorType (ArmorType.Heavy);
            Assert.Equal (2, armors.Count ());
            Assert.True (armors.All (x => x.ArmorType == ArmorType.Heavy));
        }

        [Fact]
        
        public void GetArmorsOfTypes() 
        {
            var armors = gateway.FindByArmorTypes (ArmorType.Light, ArmorType.Heavy);
            Assert.Equal (3, armors.Count ());
        }

        [Fact]
        public void GetArmorsByProficiencies() 
        {
            var proficiencies = new List<ArmorProficiency>();
            proficiencies.Add(new ArmorProficiency("Light"));
            var armors = gateway.FindByProficiency(proficiencies);
            Assert.Equal(1, armors.Count());
        }

        [Fact]
        public void ArmorHasACost()
        {
            var leather = gateway.Find("leather armor");
            Assert.Equal(2500, leather.Value);
        }
        const string ArmorYamlFile = @"
- armor:
  name: Leather Armor
  armor_class: 2
  weight: 15
  maximum_dexterity_bonus: 6
  armor_check_penalty: 0
  arcane_spell_failure_chance: 10
  armor_type: Light
  cost: 25gp
- armor:
  name: Full Plate
  armor_class: 9
  weight: 50
  maximum_dexterity_bonus: 1
  armor_check_penalty: -6
  arcane_spell_failure_chance: 35
  armor_type: Heavy
  cost: 473gp
- armor:
  name: Half Plate
  armor_class: 8
  weight: 50
  maximum_dexterity_bonus: 0
  armor_check_penalty: -7
  arcane_spell_failure_chance: 40
  armor_type: Heavy
  cost: 320gp
";
    }
}
