// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BardicPerformances
{
    using SilverNeedle.Utility;
    public class InspireCompetence : BardicPerformanceBase, IComponent
    {
        public ComponentContainer Parent { get; set; }

        private ClassLevel bardLevel;
        public int Bonus
        {
            //Extra point at level 7 and every 4 after
            get { return 2 + (bardLevel.Level - 3) / 4; }
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