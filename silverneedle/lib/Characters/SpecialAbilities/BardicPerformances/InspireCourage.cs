// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BardicPerformances
{
    using SilverNeedle.Utility;
    public class InspireCourage : BardicPerformanceBase, IComponent
    {
        public ComponentContainer Parent { get; set; }
        private ClassLevel bardLevel;
        public int Bonus
        {
            //Extra point at level 5 and every six after
            get { return 1 + (bardLevel.Level +1) / 6; }
        }

        public override string Description
        {
            get 
            {
                return string.Format("{0} {1}", base.Description, Bonus.ToModifierString());
            }
        }

        public void Initialize(ComponentContainer components)
        {
            this.bardLevel = components.Get<ClassLevel>();
        }
    }
}