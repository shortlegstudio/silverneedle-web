// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;
    using Xunit;

    public class BloodlineTests : RequiresDataFiles
    {
        private Bloodline aberrant;
        public BloodlineTests()
        {
            var configuration = bloodlineYaml.ParseYaml();
            aberrant = new Bloodline(configuration);
        }

        [Fact]
        public void AddingToCharacterGrantsNewClassSkill()
        {
            var character = CharacterTestTemplates.AverageBob().WithSkills(new string[] { "Knowledge Dungeoneering"});
            character.Add(aberrant);
            Assert.True(character.SkillRanks.GetSkill("Knowledge Dungeoneering").ClassSkill);
        }

        [Fact]
        public void ProvidesBonusSpellsDependingOnLevels()
        {
            Assert.Equal("enlarge person", aberrant.GetBonusSpell(3));
        }

        [Fact]
        public void ReturnEmptyStringForBonusSpellsWhenNothingForThatLevel()
        {
            Assert.Equal("", aberrant.GetBonusSpell(4));
        }

        [Fact]
        public void ProvidesPowersAtSpecificLevels()
        {
            Assert.Equal("SilverNeedle.Characters.Attacks.AcidicRay", aberrant.GetPower(1));
        }

        [Fact]
        public void ReturnsEmptyStringIfNoPowerForLevel()
        {
            Assert.Equal(string.Empty, aberrant.GetPower(2));
        }

        [Fact]
        public void ProvidesAListOfBonusFeats()
        {
            Assert.Equal(new string [] {
                "combat casting", "improved disarm", "improved grapple", "improved initiative",
                "improve unarmed strike", "iron will", "silent spell", "skill focus - \"knowledge (dungeoneering)\"" }
                , aberrant.GetBonusFeats()
            );
        }

        private string bloodlineYaml = @"---
name: Aberrant
class-skill: knowledge dungeoneering
bonus-spells: 
  3: enlarge person
  5: see invisibility
  7: tongues
  9: black tentacles
  11: feeblemind
  13: veil
  15: plane shift
  17: mind blank
  19: shapechange
bonus-feats: [combat casting, improved disarm, improved grapple, improved initiative, 
  improve unarmed strike, iron will, silent spell, skill focus - ""knowledge (dungeoneering)""]
powers:
  1: SilverNeedle.Characters.Attacks.AcidicRay
  3: SilverNeedle.Characters.SpecialAbilities.LongLimbs
  9: SilverNeedle.Characters.SpecialAbilities.UnusualAnatomy
  15: SilverNeedle.Characters.SpecialAbilities.AlienResistance
  20: SilverNeedle.Characters.SpecialAbilities.AberrantForm";
    }
}