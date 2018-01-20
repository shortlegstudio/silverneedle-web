// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;
    public class ScytheOfEvil : IAbility, IComponent, INameByType, IUsesPerDay
    {
        public int UsesPerDay 
        {
            get { return 1 + (sourceClass.Level - 8) /4; }
        }

        private ClassLevel sourceClass;
        public void Initialize(ComponentContainer components)
        {
            this.sourceClass = components.Get<ClassLevel>();
        }

        public string DisplayString()
        {
            return "{0}/day - {1}".Formatted(this.UsesPerDay, this.Name());
        }
    }
}