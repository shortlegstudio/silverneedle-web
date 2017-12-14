// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Background;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Serialization;
    using SilverNeedle.Spells;
    using SilverNeedle.Utility;
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

        public static CharacterSheet WithSkills(this CharacterSheet sheet)
        {
            var skills = GatewayProvider.All<Skill>().Select(x => x.Name);
            return sheet.WithSkills(skills);
        }
        public static CharacterSheet WithSkills(this CharacterSheet sheet, IEnumerable<string> names)
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

        private static CharacterSheet CreateWithClass(string className)
        {
            var character = CreateWithAverageAbilityScores();
            var cls = new Class(className);
            character.SetClass(cls);
            return character;
        }

        public static CharacterSheet DruidDonna()
        {
            return CreateWithClass("Druid");
        }
        public static CharacterSheet Cleric()
        {
            return CreateWithClass("Cleric");
        }

        public static CharacterSheet MarkyMonk()
        {
            return CreateWithClass("Monk");
        }

        public static CharacterSheet StrongBad()
        {
            var bad = CreateWithAverageAbilityScores();
            bad.AbilityScores.SetScore(AbilityScoreTypes.Strength, 16);
            return bad;
        }

        public static CharacterSheet BardyBard()
        {
            return CreateWithClass("Bard");
        }

        public static CharacterSheet Sorcerer()
        {
            return CreateWithClass("sorcerer");
        }

        public static CharacterSheet Wizard()
        {
            return CreateWithClass("wizard");
        }

        public static CharacterSheet Ranger()
        {
            return CreateWithClass("ranger");
        }

        public static CharacterSheet WithSpontaneousCasting(this CharacterSheet character)
        {
            var spellcastingConfigurationYaml = @"---
list: " + character.Class.Name + @"
type: arcane
casting-ability: charisma
spell-slots:
  1: [4, 1]
  2: [5, 2]
  3: [6, 3, 2]
spells-known:
  1: [4, 2]
  2: [5, 3]
  3: [6, 4, 1]
";
            var spellcastingConfiguration = spellcastingConfigurationYaml.ParseYaml();
            var spellList = CreateGenericSpellList(character.Class.Name);
            var spellCasting = new SpontaneousCasting(spellcastingConfiguration, EntityGateway<SpellList>.LoadWithSingleItem(spellList));
            character.Add(spellCasting);
            return character;

        }

        public static CharacterSheet WithWizardCasting(this CharacterSheet character)
        {
            var spellcastingConfigurationYaml = @"---
list: " + character.Class.Name + @"
type: arcane
casting-ability: intelligence
spell-slots:
  1: [3, 2]
  2: [4, 3]
  3: [4, 3, 2]
";
            var spellList = CreateGenericSpellList(character.Class.Name);
            character.Add(new WizardCasting(spellcastingConfigurationYaml.ParseYaml(), EntityGateway<SpellList>.LoadWithSingleItem(spellList)));
            return character;
        }
        public static CharacterSheet WithDivineCasting(this CharacterSheet character)
        {
            var spellcastingConfigurationYaml = @"---
list: " + character.Class.Name + @"
type: divine
casting-ability: wisdom
spell-slots:
  1: [3, 2]
  2: [4, 3]
  3: [4, 3, 2]
";
            var spellList = CreateGenericSpellList(character.Class.Name);
            character.Add(new DivineCastingNew(spellcastingConfigurationYaml.ParseYaml(), EntityGateway<SpellList>.LoadWithSingleItem(spellList)));
            return character;
        }

        public static CharacterSheet WithDivineCastingNoOrisons(this CharacterSheet character)
        {
            var spellcastingConfigurationYaml = @"---
list: " + character.Class.Name + @"
type: divine
casting-ability: wisdom
spell-slots:
  1: [0, 2]
  2: [0, 3]
  3: [0, 3, 2]
";
            var spellList = CreateGenericSpellList(character.Class.Name);
            character.Add(new DivineCastingNew(spellcastingConfigurationYaml.ParseYaml(), EntityGateway<SpellList>.LoadWithSingleItem(spellList)));
            return character;
        }

        private static SpellList CreateGenericSpellList(string className)
        {
            var spellList = new SpellList();
            spellList.Class = className;
            //Add a bunch of spells
            for(int level = 0; level < 10; level++)
            {
                for(int spellCount = 0; spellCount < 15; spellCount++)
                {
                    spellList.Add(level, string.Format("spell {0}-{1}", level, spellCount));
                }
            }

            return spellList;
        }
    }
}