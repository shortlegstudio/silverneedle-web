// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using SilverNeedle.Characters;
    public static class CharacterSheetHelpers
    {
        public static CharacterSheet CreateBlankStandardOGLSheet()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            //Fill in the standard skills to be expected
            var skills = new Skill[] 
            {
                new Skill("Acrobatics", AbilityScoreTypes.Dexterity, false),
                new Skill("Appraise", AbilityScoreTypes.Intelligence, false),
                new Skill("Bluff", AbilityScoreTypes.Charisma, false),
                new Skill("Climb", AbilityScoreTypes.Strength, false),
                new Skill("Craft", AbilityScoreTypes.Intelligence, false),
                new Skill("Diplomacy", AbilityScoreTypes.Charisma, false),
                new Skill("Disable Device", AbilityScoreTypes.Dexterity, true),
                new Skill("Disguise", AbilityScoreTypes.Charisma, false),
                new Skill("Escape Artist", AbilityScoreTypes.Dexterity, false),
                new Skill("Fly", AbilityScoreTypes.Dexterity, false),
                new Skill("Handle Animal", AbilityScoreTypes.Charisma, true),
                new Skill("Heal", AbilityScoreTypes.Wisdom, false),
                new Skill("Intimidate", AbilityScoreTypes.Charisma, false),
                new Skill("Knowledge (arcana)", AbilityScoreTypes.Intelligence, true),
                new Skill("Knowledge (dungeoneering)", AbilityScoreTypes.Intelligence, true),
                new Skill("Knowledge (engineering)", AbilityScoreTypes.Intelligence, true),
                new Skill("Knowledge (geography)", AbilityScoreTypes.Intelligence, true),
                new Skill("Knowledge (history)", AbilityScoreTypes.Intelligence, true),
                new Skill("Knowledge (local)", AbilityScoreTypes.Intelligence, true),
                new Skill("Knowledge (nature)", AbilityScoreTypes.Intelligence, true),
                new Skill("Knowledge (nobility)", AbilityScoreTypes.Intelligence, true),
                new Skill("Knowledge (planes)", AbilityScoreTypes.Intelligence, true),
                new Skill("Knowledge (religion)", AbilityScoreTypes.Intelligence, true),
                new Skill("Linguistics", AbilityScoreTypes.Intelligence, true),
                new Skill("Perception", AbilityScoreTypes.Wisdom, false),
                new Skill("Perform", AbilityScoreTypes.Charisma, false),
                new Skill("Profession", AbilityScoreTypes.Wisdom, true),
                new Skill("Ride", AbilityScoreTypes.Dexterity, false),
                new Skill("Sense Motive", AbilityScoreTypes.Charisma, false),
                new Skill("Sleight of Hand", AbilityScoreTypes.Dexterity, true),
                new Skill("Spellcraft", AbilityScoreTypes.Intelligence, true),
                new Skill("Stealth", AbilityScoreTypes.Dexterity, false),
                new Skill("Survival", AbilityScoreTypes.Wisdom, false),
                new Skill("Swim", AbilityScoreTypes.Strength, false),
                new Skill("Use Magical Device", AbilityScoreTypes.Charisma, true)
            };

            character.SkillRanks.FillSkills(skills);

            character.InitializeComponents();
            return character;
        }
    }
}