// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

namespace Tests.Characters
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    
    public class CharacterBuildStrategyTests
    {
        EntityGateway<CharacterBuildStrategy> strategies;

        public CharacterBuildStrategyTests()
        {
            var list = CharacterBuildYaml.ParseYaml().Load<CharacterBuildStrategy>();
            strategies = EntityGateway<CharacterBuildStrategy>.LoadFromList(list);
        }

        [Fact]
        public void DefaultTargetLevelIsOne()
        {
            var strat = new CharacterBuildStrategy();
            Assert.Equal(1, strat.TargetLevel);
        }

        [Fact]
        public void LoadsWeightedTablesForRacesAndClasses()
        {
            var archer = strategies.Find("Archer");
            Assert.Equal(10, archer.Races.All.First().MaximumValue);
            Assert.Equal("elf", archer.Races.All.First().Option);
            Assert.Equal(10, archer.Classes.All.First().MaximumValue);
            Assert.Equal("Fighter", archer.Classes.All.First().Option);
        }  
        
        [Fact]
        public void StrategyProvidesGuidanceOnFavoringClassSkills()
        {
            var tank = strategies.Find("tank");
            Assert.Equal(3.2f, tank.ClassSkillMultiplier);
        }

        [Fact]
        public void IgnoreCaseMatching()
        {
            var tank = strategies.Find("tank");
            Assert.NotNull(tank);
        }

        [Fact]
        public void StrategyProvidesBaseForAllSkills()
        {
            var archer = strategies.Find("archer");
            Assert.Equal(1, archer.BaseSkillWeight);
        }

        [Fact]
        public void StrategyProvidesSpecificationOnSkills()
        {
            var archer = strategies.Find("archer");
            Assert.NotNull(archer.FavoredSkills);
            Assert.Equal("survival", archer.FavoredSkills.All.First().Option);
            Assert.Equal(20, archer.FavoredSkills.All.First().MaximumValue);
        }

        [Fact]
        public void StrategyProvidesRecommendationsOnFeats()
        {
            var tank = strategies.Find("tank");
            var feats = tank.FavoredFeats.All;
            
            Assert.Equal("power attack", feats.First().Option);
            Assert.Equal(100, feats.First().MaximumValue);

            Assert.Equal(3, feats.Count());
        }

        [Fact]
        public void StrategyFavorsSomeAttributesAheadOfOthers() 
        {
            var tank = strategies.Find("tank");
            var abilities = tank.FavoredAbilities.All;
            Assert.Equal(AbilityScoreTypes.Strength, abilities.First().Option);
            Assert.Equal(100, abilities.First().MaximumValue);
            Assert.Equal(6, abilities.Count());
        }

        [Fact]
        public void StrategyProvidesDefaultsToAbilitiesNotSpecifiedOfOne()
        {
            var archer = strategies.Find("archer");
            var abilities = archer.FavoredAbilities.All;
            Assert.Equal(AbilityScoreTypes.Strength, abilities.First().Option);
            Assert.Equal(AbilityScoreTypes.Charisma, abilities.Last().Option);
            Assert.Equal(1, abilities.First().MaximumValue);
            Assert.Equal(6, abilities.Last().MaximumValue);
            Assert.Equal(6, abilities.Count());
        }

        [Fact]
        public void AnEmptyStrategySelectsAllAttributesEvenly()
        {
            var strategy = new CharacterBuildStrategy();
            Assert.Equal(6, strategy.FavoredAbilities.All.Count());
        }

        [Fact]
        public void SpecifiesTheDesignerToUseCreatingCharacter()
        {
            var archer = strategies.Find("archer");
            Assert.Equal("create-default", archer.Designer);
        }

        [Fact]
        public void DefaultAbilityScoreGeneratorToAverageAbilities()
        {
            var strategy = new CharacterBuildStrategy();
            Assert.Contains("AverageAbilityScore", strategy.AbilityScoreRoller);
        }

        [Fact]
        public void CanSpecifyDifferentAbilityScoreGeneratorsInStrategy()
        {
            var archer = strategies.Find("archer");
            Assert.Contains("HeroicAbilityScore", archer.AbilityScoreRoller);
        }

        private const string CharacterBuildYaml = @"--- 
- build:
  name: Archer
  ability-score-roller: SilverNeedle.Actions.CharacterGenerator.Abilities.HeroicAbilityScoreGenerator
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
  designer: create-default
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
  designer: equip-adventurer
";
    }
}