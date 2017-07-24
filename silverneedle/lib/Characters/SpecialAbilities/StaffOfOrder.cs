// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    public class StaffOfOrder : SpecialAbility
    {
        public int UsesPerDay 
        {
            get { return 1 + (sourceClass.Level - 8) /4; }
        }

        private ClassLevel sourceClass;
        public StaffOfOrder(ClassLevel sourceClass)
        {
            this.sourceClass = sourceClass;
        }
    }
}