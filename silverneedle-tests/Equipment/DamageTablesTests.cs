// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using System;
using Xunit;
using SilverNeedle.Characters;
using SilverNeedle.Equipment;

namespace Equipment {

    public class DamageTablesTests {
        [Fact]
        public void DamageStatisticsCanBeConvertedBasedOnSize() {
            Assert.Equal ("1d4", DamageTables.ConvertDamageBySize("1d6", CharacterSize.Small));
            Assert.Equal ("2d6", DamageTables.ConvertDamageBySize ("1d8", CharacterSize.Large));
            Assert.Equal ("2d6", DamageTables.ConvertDamageBySize ("2d10", CharacterSize.Tiny));
            Assert.Equal ("1d6", DamageTables.ConvertDamageBySize ("1d8", CharacterSize.Small));
            Assert.Equal ("1d8", DamageTables.ConvertDamageBySize ("1d8", CharacterSize.Medium));
        }

        [Fact]
        public void DropsModifiersDuringConversion()
        {
            Assert.Equal ("1d4+2", DamageTables.ConvertDamageBySize("1d6+2", CharacterSize.Small));
        }


        [Fact]
        public void NotImplementedExceptionTriggeredForNotSupportedSizes() {
            Assert.Throws(typeof(NotImplementedException), () => DamageTables.ConvertDamageBySize ("1d6", CharacterSize.Colossal));
        }

        [Fact]
        public void ThrowsDamageTableAmountNotFoundExceptionIfIncorrectTypesAreUsed()
        {
            Assert.Throws(typeof(DamageTableValueNotFoundException), () => DamageTables.ConvertDamageBySize("10d6", CharacterSize.Small));
        }

    }
}