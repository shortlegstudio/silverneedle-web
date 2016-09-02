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
        private IEntityGateway<Class> classes; 

        public ClassSelector (IEntityGateway<Class> classes)
        {
            this.classes = classes;
        }
    
        public void ChooseClass(CharacterSheet character)
        {
            AssignClass(character, classes.All().ToList().ChooseOne());
        }

        public void ChooseClass(CharacterSheet character, WeightedOptionTable<string> classChoices)
        {
            var choice = classChoices.ChooseRandomly();
            var cls = classes.All().First(x => x.Name == choice);
            AssignClass(character, cls);
        }   

        private void AssignClass(CharacterSheet character, Class cls) 
        {
            character.SetClass(cls);
            
            var hpRoller = new HitPointRoller();
            var hp = hpRoller.AddMaxHitPoints(character);
        }
    }
}