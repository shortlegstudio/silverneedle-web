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
            var cls = classes.All().ToList().ChooseOne();
            character.SetClass(cls);
            
            var hpRoller = new HitPointRoller();
            var hp = hpRoller.AddMaxHitPoints(character);
        }
    }
}