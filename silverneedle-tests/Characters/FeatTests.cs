// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters {
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Prerequisites;
    using SilverNeedle.Utility;
    
    
    public class FeatTests {
        Feat Acrobatic;
        Feat CombatExpertise;
        Feat PowerAttack;
        Feat CraftWand;


        public FeatTests() {
            var data = FeatYamlFile.ParseYaml();
            var list = new List<Feat>();
            foreach(var c in data.Children)
            {
                list.Add(new Feat(c));
            }

            Acrobatic = list.First(x => x.Name == "Acrobatic");
            CombatExpertise = list.First(x => x.Name == "Combat Expertise");
            PowerAttack = list.First(x => x.Name == "Power Attack");
            CraftWand = list.First(x => x.Name == "Craft Wand");
        }
            

        [Fact]
        public void FeatsKnowWhetherYouQualify() {
            var smartCharacter = new CharacterSheet ();
            smartCharacter.AbilityScores.SetScore (AbilityScoreTypes.Intelligence, 15);
            var dumbCharacter = new CharacterSheet ();
            dumbCharacter.AbilityScores.SetScore (AbilityScoreTypes.Intelligence, 5);

            var CombatExpertise = new Feat();
            CombatExpertise.Prerequisites.Add(new AbilityPrerequisite(AbilityScoreTypes.Intelligence, 13));

            Assert.True (CombatExpertise.IsQualified (smartCharacter));
            Assert.False (CombatExpertise.IsQualified (dumbCharacter));
        }		

        [Fact]
        public void IfFeatIsAlreadySelectedItCannotBeSelectedAgain()
        {
            var character = new CharacterSheet ();
            var basicFeat = new Feat();
            character.Add(basicFeat);
            Assert.False(basicFeat.IsQualified(character));
        }

        [Fact]
        public void MatchesByName()
        {
            Assert.True(Acrobatic.Matches("Acrobatic"));
            Assert.True(Acrobatic.Matches("acrobatic"));
        }
        
        [Fact]
        public void FeatsHaveADescription() {
            Assert.Equal ("Move good", Acrobatic.Description);
            Assert.Equal ("Hit Stuff Hard", PowerAttack.Description);
        }

        [Fact]
        public void FeatsCanHaveSkillAdjustments() {
            var modifiers = Acrobatic.Modifiers;
            Assert.Equal (2, modifiers.Count);
            var skillAdj = modifiers.First ();
            Assert.Equal ("Acrobatics", skillAdj.StatisticName);
            Assert.Equal("bonus", skillAdj.Type);
            Assert.Equal ("Acrobatic (feat)", skillAdj.Reason);
            Assert.Equal (2, skillAdj.Modifier);

            var flyAdj = modifiers.Last ();
            Assert.Equal ("Fly", flyAdj.StatisticName);
            Assert.Equal("bonus", skillAdj.Type);
            Assert.Equal ("Acrobatic (feat)", skillAdj.Reason);
            Assert.Equal (4, flyAdj.Modifier);
        }

        [Fact]
        public void FeatsCanHaveAbilityPrerequisites() {
            var prereq = CombatExpertise.Prerequisites;
            var abilityCheck = prereq.First() as AbilityPrerequisite;
            Assert.IsType<AbilityPrerequisite> (abilityCheck);
            Assert.Equal (AbilityScoreTypes.Intelligence, abilityCheck.Ability);
            Assert.Equal (13, abilityCheck.Minimum);
        }

        [Fact]
        public void SomeFeatsAreCombatFeats() {
            Assert.True (CombatExpertise.IsCombatFeat);
            Assert.False (Acrobatic.IsCombatFeat);
        }

        [Fact]
        public void SomeFeatsAreCriticalFeats() {
            Assert.True (PowerAttack.IsCriticalFeat);
            Assert.False (Acrobatic.IsCriticalFeat);
        }

        [Fact]
        public void SomeFeatsAreItemCreationFeats() {
            Assert.True (CraftWand.IsItemCreation);
            Assert.False (Acrobatic.IsItemCreation);
        }

        private const string FeatYamlFile = @"--- 
- feat: 
  name: Acrobatic
  description: Move good
  modifiers:
    - type: bonus
      stat: Acrobatics
      modifier: 2
    - type: bonus
      stat: Fly
      modifier: 4
- feat:
  name: Combat Expertise
  description: Dodge stuff better
  tags: combat
  prerequisites:
    - intelligence: 13
- feat:
  name: Power Attack
  description: Hit Stuff Hard
  tags: combat, critical
- feat:
  name: Craft Wand
  tags: itemcreation
  description: Make Wands
...";
    }
}
