// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;

    public class DimensionalSteps : SpecialAbility, IComponent
    {
        private ClassLevel sourceLevel;
        public void Initialize(ComponentBag components)
        {
            sourceLevel = components.Get<ClassLevel>();
        }

        public int Distance
        {
            get { return 30 * sourceLevel.Level; }
        }

        public override string Name
        {
            get 
            {
                 return "{0} ({1} ft/day)".Formatted(base.Name, Distance);
            }
        }
    }
}