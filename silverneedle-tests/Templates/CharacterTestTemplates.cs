// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Background;
    public static class CharacterTestTemplates
    {
        private static CharacterSheet CreateWithAverageAbilityScores()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            foreach(var s in character.AbilityScores.Abilities)
            {
                s.SetValue(10);
            }
            character.InitializeComponents();
            return character;
        }
        public static CharacterSheet WithSkills(this CharacterSheet sheet, string[] names)
        {
            foreach(var n in names)
            {
                sheet.SkillRanks.AddSkill(new Skill(n, AbilityScoreTypes.Wisdom, false));
            }
            return sheet;
        }
        public static CharacterSheet WithSkills(string[] names)
        {
            return CreateWithAverageAbilityScores().WithSkills(names);
        }
        public static CharacterSheet AverageBob()
        {
            var bob = CreateWithAverageAbilityScores();
            bob.FirstName = "Bob";
            bob.Gender = Gender.Male;
            bob.Get<History>().FamilyTree.Father.FirstName = "Bob's Father";
            bob.Get<History>().FamilyTree.Mother.FirstName = "Bob's Mother";

            var human = new Race();
            human.Name = "Human";
            bob.SetRace(human);
            return bob;
        }

        public static CharacterSheet DruidDonna()
        {
            var donna = CreateWithAverageAbilityScores();
            var druid = new Class("Druid");
            donna.SetClass(druid);

            return donna;
        }

        public static CharacterSheet MarkyMonk()
        {
            var marky = CreateWithAverageAbilityScores();
            var monk = new Class("Monk");
            marky.SetClass(monk);
            return marky;
        }

        public static CharacterSheet StrongBad()
        {
            var bad = CreateWithAverageAbilityScores();
            bad.AbilityScores.SetScore(AbilityScoreTypes.Strength, 16);
            return bad;
        }

        public static CharacterSheet BardyBard()
        {
            var bardy = CreateWithAverageAbilityScores();
            var bard = new Class("Bard");
            bardy.SetClass(bard);
            return bardy;
        }

    }
}