// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace Tests.Characters 
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;
    
    public class ArmorProficiencyTests {
        [Fact]
        public void ArmorTypesCanBeDefinedAsProficieny() {
            var prof = new ArmorProficiency("Light");
            var armor = new Armor();
            armor.ArmorType = ArmorType.Light;

            Assert.True(prof.IsProficient(armor));
            armor.ArmorType = ArmorType.Heavy;

            Assert.False(prof.IsProficient(armor));
        }


        [Fact]
        public void MatchesBasedOnNameIfNotTrainingLevel() {
            var prof = new ArmorProficiency("Hide");
            var armor = new Armor();
            armor.Name = "Leather";
            Assert.False(prof.IsProficient(armor));
            armor.Name = "Hide";
            Assert.True(prof.IsProficient(armor));
        }

        [Fact]
        public void CanLoadAYamlListThatProvidesProficiencyInformation()
        {
            var yaml = @"---
armors: [light, medium]";
            var prof = new ArmorProficiency(yaml.ParseYaml());
            var light = new Armor();
            light.ArmorType = ArmorType.Light;
            var medium = new Armor();
            medium.ArmorType = ArmorType.Medium;
            var heavy = new Armor();
            heavy.ArmorType = ArmorType.Heavy;
            Assert.True(prof.IsProficient(light));
            Assert.True(prof.IsProficient(medium));
            Assert.False(prof.IsProficient(heavy));
        }
    }
}