// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    public class Unity : SpecialAbility
    {
        private ClassLevel sourceClass;
        public Unity(ClassLevel source)
        {
            this.sourceClass = source;
        }

        public int UsesPerDay
        {
            get { return 1 + (sourceClass.Level - 8) / 4; }
        }
    }
}