// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

namespace Characters
{
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Yaml;

    [TestFixture]
    public class CharacterBuildYamlGatewayTests
    {
        CharacterBuildYamlGateway gateway;
        [SetUp]
        public void SetUp()
        {
            gateway = new CharacterBuildYamlGateway(CharacterBuildYaml.ParseYaml()); 
        }

        [Test]
        public void LoadsWeightedTablesForRacesAndClasses()
        {
            var archer = gateway.GetBuild("Archer");
            Assert.AreEqual(10, archer.Races.All().First().MaximumValue);
            Assert.AreEqual("elf", archer.Races.All().First().Option);
            Assert.AreEqual(10, archer.Classes.All().First().MaximumValue);
            Assert.AreEqual("Fighter", archer.Classes.All().First().Option);
        }  
        
        [Test]
        public void StrategyProvidesGuidanceOnFavoringClassSkills()
        {
            var tank = gateway.GetBuild("tank");
            Assert.AreEqual(3.2f, tank.ClassSkillMultiplier);
        }

        [Test]
        public void IgnoreCaseMatching()
        {
            var tank = gateway.GetBuild("tank");
            Assert.IsNotNull(tank);
        }

        private const string CharacterBuildYaml = @"--- 
- build:
  name: Archer
  races:
    - name: elf
      weight: 10
    - name: human
      weight: 12
  classes:
    - name: Fighter
      weight: 10
    - name: Ranger
      weight: 15
    - name: Rogue
      weight: 5
  classskillmultiplier: 2
  baseskillweight: 1
  skills:
    - name: survival
      weight: 20
    - name: climb
      weight: 16
    - name: perception
      weight: 16    
- build:
  name: Tank
  races:
    - name: halfling
      weight: 12
    - name: half-orc
      weight: 10
  classes:
    - name: Fighter
      weight: 10
    - name: Paladin
      weight: 8
    - name: Ranger
      weight: 6
    - name: Monk
      weight: 6
  classskillmultiplier: 3.2
  baseskillweight: 1
  skills:
    - name: survival
      weight: 20
    - name: climb
      weight: 16
    - name: perception
      weight: 16    
";
    }
}