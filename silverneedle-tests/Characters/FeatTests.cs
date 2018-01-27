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
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;
    
    
    public class FeatTests {
        Feat Acrobatic;
        Feat CombatExpertise;
        Feat PowerAttack;
        Feat CraftWand;
        Feat MultipleSelect;


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
            MultipleSelect = list.First(x => x.Name == "Multiple Select");
        }
            

        [Fact]
        public void FeatsKnowWhetherYouQualify() {
            var smartCharacter = CharacterTestTemplates.AverageBob();
            smartCharacter.AbilityScores.SetScore (AbilityScoreTypes.Intelligence, 15);
            var dumbCharacter = CharacterTestTemplates.AverageBob();
            dumbCharacter.AbilityScores.SetScore (AbilityScoreTypes.Intelligence, 5);

            var CombatExpertise = Feat.Named("Combat Expertise");
            CombatExpertise.Prerequisites.Add(new AbilityPrerequisite(AbilityScoreTypes.Intelligence, 13));

            Assert.True (CombatExpertise.IsQualified (smartCharacter));
            Assert.False (CombatExpertise.IsQualified (dumbCharacter));
        }		

        [Fact]
        public void IfFeatIsAlreadySelectedItCannotBeSelectedAgain()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            var cr1 = Feat.Named("Combat Reflexes");
            var cr2 = Feat.Named("Combat Reflexes");
            Assert.True(cr2.IsQualified(character));
            character.Add(cr1);
            Assert.False(cr2.IsQualified(character));
        }

        [Fact]
        public void IfFeatHasPrerequisitesTheyCanBeBypassed()
        {
            var CombatExpertise = Feat.Named("Combat Expertise");
            CombatExpertise.Prerequisites.Add(new AbilityPrerequisite(AbilityScoreTypes.Intelligence, 13));
            var character = CharacterTestTemplates.AverageBob();
            Assert.False(CombatExpertise.IsQualified(character));
            Assert.True(CombatExpertise.IsQualifiedIgnorePrerequisites(character));
        }

        [Fact]
        public void IgnoringPrerequisitesStillRequiresUniqueFeats()
        {
            var feat = Feat.Named("Testing");
            var copy = feat.Copy();
            var character = CharacterTestTemplates.AverageBob();
            character.Add(feat);
            Assert.False(copy.IsQualifiedIgnorePrerequisites(character));
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

        [Fact]
        public void SomeFeatsAllowYouToSelectMultipleTimes()
        {
            Assert.True(MultipleSelect.AllowMultiple);
        }

        [Fact]
        public void SupportCopying()
        {
            var feat = Acrobatic.Copy();
            Assert.Equal(Acrobatic.Name, feat.Name);
            Assert.Equal(Acrobatic.Description, feat.Description);
            Assert.Equal(Acrobatic.Modifiers.Count(), feat.Modifiers.Count());
            var multi = MultipleSelect.Copy();
            Assert.True(multi.AllowMultiple);
        }

        [Fact]
        public void ComparesFeatsByNameWhetherTheyAreEqual()
        {
            var copy = Acrobatic.Copy();
            Assert.True(copy.Equals(Acrobatic));
        }

        [Fact]
        public void AllowsMultipleFeatsToBeQualifiedForCharacter()
        {
            var multi1 = MultipleSelect.Copy();
            var multi2 = MultipleSelect.Copy();

            var character = CharacterTestTemplates.AverageBob();
            character.Add(multi1);
            Assert.True(multi2.IsQualified(character));
        }

        private const string FeatYamlFile = @"--- 
- feat: 
  name: Acrobatic
  description: Move good
- feat:
  name: Combat Expertise
  description: Dodge stuff better
  tags: [ combat ]
  prerequisites:
    - intelligence: 13
- feat:
  name: Power Attack
  description: Hit Stuff Hard
  tags: [combat, critical]
- feat:
  name: Craft Wand
  tags: [ itemcreation ]
  description: Make Wands
- feat:
  name: Multiple Select
  allow-multiple: true
...";
    }
}
