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
            var character = new CharacterSheet();
            foreach(var s in character.AbilityScores.Abilities)
            {
                s.SetValue(10);
            }
            return character;
        }
        public static CharacterSheet WithSkills(string[] names)
        {
            var skills = CreateWithAverageAbilityScores();
            foreach(var n in names)
            {
                skills.SkillRanks.AddSkill(new Skill(n, AbilityScoreTypes.Wisdom, false));
            }
            return skills;
        }
        public static CharacterSheet AverageBob()
        {
            var bob = CreateWithAverageAbilityScores();
            bob.InitializeComponents();
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
            donna.InitializeComponents();
            var druid = new Class("Druid");
            donna.SetClass(druid);

            return donna;
        }

        public static CharacterSheet MarkyMonk()
        {
            var marky = CreateWithAverageAbilityScores();
            marky.InitializeComponents();
            var monk = new Class("Monk");
            marky.SetClass(monk);
            return marky;
        }

    }
}