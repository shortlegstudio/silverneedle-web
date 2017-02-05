// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

using System.Linq;
using SilverNeedle.Characters;

namespace SilverNeedle.Actions.CharacterGenerator
{
    public class ClassSelector
    {
        private IClassGateway classes; 

        public ClassSelector (IClassGateway classes)
        {
            this.classes = classes;
        }
    
        public void ChooseAny(CharacterSheet character)
        {
            AssignClass(character, classes.All().ToList().ChooseOne());
        }

        public void ChooseClass(CharacterSheet character, WeightedOptionTable<string> classChoices)
        {
            if (classChoices.IsEmpty)
            {
                ChooseAny(character);
                return;
            }
            
            var choice = classChoices.ChooseRandomly();
            var cls = classes.GetByName(choice);
            AssignClass(character, cls);
        }   

        public void AssignClass(CharacterSheet character, Class cls) 
        {
            character.SetClass(cls);
            
            var hpRoller = new HitPointRoller();
            var hp = hpRoller.AddMaxHitPoints(character);

            foreach(var skill in cls.ClassSkills)
            {
                character.SkillRanks.SetClassSkill(skill);
            }
            var firstClassLevel = cls.GetLevel(1);
            character.ProcessSpecialAbilities(firstClassLevel);            
        }
    }
}