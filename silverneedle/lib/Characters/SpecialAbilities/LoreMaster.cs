// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;
    public class LoreMaster : SpecialAbility, IComponent
    {
        private ClassLevel bardLevels;
        public int UsesPerDay
        {
            get 
            {
                return 1 + (bardLevels.Level - 5) / 6;
            }
        }
        public void Initialize(ComponentBag components)
        {
            bardLevels = components.Get<ClassLevel>();
        }

        public override string Name
        {
            get
            {
                if(bardLevels == null)
                    return base.Name;
                
                return "{0} {1}/day".Formatted(base.Name, UsesPerDay);
            }
        }
    }
}