// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    public class DancingWeapons : SpecialAbility
    {
        private ClassLevel sourceClass;
        public int UsesPerDay 
        {
            get { return 1 + (sourceClass.Level - 8) / 4; }
        }

        public DancingWeapons(ClassLevel sourceClass)
        {
            this.sourceClass = sourceClass;
        }
    }
}