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
    public class CharacterBuildGatewayTests
    {
        CharacterBuildGateway gateway;
        [SetUp]
        public void SetUp()
        {
            gateway = new CharacterBuildGateway(CharacterBuildYaml.ParseYaml()); 
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

        [Test]
        public void StrategyProvidesBaseForAllSkills()
        {
            var archer = gateway.GetBuild("archer");
            Assert.AreEqual(1, archer.BaseSkillWeight);
        }

        [Test]
        public void StrategyProvidesSpecificationOnSkills()
        {
            var archer = gateway.GetBuild("archer");
            Assert.IsNotNull(archer.FavoredSkills);
            Assert.AreEqual("survival", archer.FavoredSkills.All().First().Option);
            Assert.AreEqual(20, archer.FavoredSkills.All().First().MaximumValue);
        }

        [Test]
        public void StrategyProvidesRecommendationsOnFeats()
        {
            var tank = gateway.GetBuild("tank");
            var feats = tank.FavoredFeats.All();
            
            Assert.AreEqual("power attack", feats.First().Option);
            Assert.AreEqual(100, feats.First().MaximumValue);

            Assert.AreEqual(3, feats.Count());
        }

        [Test]
        public void StrategyFavorsSomeAttributesAheadOfOthers() 
        {
            var tank = gateway.GetBuild("tank");
            var abilities = tank.FavoredAbilities.All();
            Assert.AreEqual(AbilityScoreTypes.Strength, abilities.First().Option);
            Assert.AreEqual(100, abilities.First().MaximumValue);
            Assert.AreEqual(6, abilities.Count());
        }

        [Test]
        public void StrategyProvidesDefaultsToAbilitiesNotSpecifiedOfOne()
        {
            var archer = gateway.GetBuild("archer");
            var abilities = archer.FavoredAbilities.All();
            Assert.AreEqual(AbilityScoreTypes.Strength, abilities.First().Option);
            Assert.AreEqual(AbilityScoreTypes.Charisma, abilities.Last().Option);
            Assert.AreEqual(1, abilities.First().MaximumValue);
            Assert.AreEqual(6, abilities.Last().MaximumValue);
            Assert.AreEqual(6, abilities.Count());
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
  feats:
    - name: point-blank shot
      weight: 20
    - name: quick draw
      weight: 10
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
  feats:
    - name: power attack
      weight: 100
    - name: cleave
      weight: 40
    - name: shield focus
      weight: 10
  abilities:
    - name: strength
      weight: 100
    - name: dexterity
      weight: 50
";
    }
}