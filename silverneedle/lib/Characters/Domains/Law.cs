// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Domains
{
    using System;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class Law : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel source;
        private TouchOfLaw touchLaw;
        private StaffOfOrder staffOfOrder;

        public Law(IObjectStore data) : base(data)
        {
            
        }

        public void Initialize(ComponentBag components)
        {
            source = components.Get<ClassLevel>();
            touchLaw = new TouchOfLaw(components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom));
            components.Add(touchLaw);
        }

        public void LeveledUp(ComponentBag components)
        {
            if(source.Level == 8)
            {
                staffOfOrder = new StaffOfOrder(source);
                components.Add(staffOfOrder);
            }
        }
    }
}