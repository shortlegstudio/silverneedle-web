// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;

    public class LongLimbs : SpecialAbility, IBloodlinePower, IComponent
    {
        private ClassLevel sourceClass;
        public void Initialize(ComponentBag components)
        {
            sourceClass = components.Get<ClassLevel>();
        }

        public override string Name 
        {
            get
            {
                return "{0} ({1} ft)".Formatted(base.Name, this.Reach);
            }
        }

        public int Reach
        {
            get
            {
                if(sourceClass.Level >= 17)
                    return 15;
                if(sourceClass.Level >= 11)
                    return 10;
                return 5;
            }
        }
    }
}