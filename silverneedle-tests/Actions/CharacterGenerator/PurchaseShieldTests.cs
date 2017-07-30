// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions {
    using System.Linq;
    using System.Collections.Generic;
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Actions.CharacterGenerator;
    using SilverNeedle.Equipment;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    
    public class PurchaseShieldTests
    {
        EntityGateway<Armor> gateway;

        CharacterSheet proficientCharacter;

        CharacterSheet incapableCharacter;        

        PurchaseShield subject;
        Armor shield;

        public PurchaseShieldTests()
        {
            var armors = new List<Armor>();
            shield = new Armor();
            shield.ArmorType = ArmorType.Shield;

            armors.Add(shield);
            gateway = new EntityGateway<Armor>(armors);
            proficientCharacter = new CharacterSheet();
            proficientCharacter.Defense.AddArmorProficiencies(new string[] {"shield"});
            incapableCharacter = new CharacterSheet();

            subject = new PurchaseShield(gateway);
        }

        [Fact]
        public void PurchaseAShield () {
            subject.Process (proficientCharacter, new CharacterBuildStrategy());

            Assert.Single (proficientCharacter.Inventory.Armor, shield);
        }

        [Fact]
        public void DoesNotEquipShieldIfNotProficient()
        {
            subject.Process(incapableCharacter, new CharacterBuildStrategy());
            Assert.True(incapableCharacter.Inventory.Armor.None(x => x == shield));
        }

        [Fact]
        public void PurchasingArmorCostsMoney()
        {
            shield.Value = 3700;
            proficientCharacter.Inventory.CoinPurse.SetValue(4000);

            subject.Process(proficientCharacter, new CharacterBuildStrategy());

            Assert.Equal(proficientCharacter.Inventory.CoinPurse.Value, 300);
            Assert.Single(proficientCharacter.Inventory.Armor, shield);
        }

        [Fact]
        public void DoNotPurchaseTooExpensiveItems()
        {
            shield.Value = 60000;
            proficientCharacter.Inventory.CoinPurse.SetValue(4000);

            subject.Process(proficientCharacter, new CharacterBuildStrategy());

            Assert.Equal(proficientCharacter.Inventory.CoinPurse.Value, 4000);
            Assert.True(proficientCharacter.Inventory.Armor.None(x => x.Equals(shield)));

        }
    }
}

