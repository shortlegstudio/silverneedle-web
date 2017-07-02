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

    public class Glory : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel source;
        private TouchOfGlory touchGlory;
        private DivinePresence divPres;

        public Glory(IObjectStore data) : base(data)
        {
            
        }

        public void Initialize(ComponentBag components)
        {
            source = components.Get<ClassLevel>();
            touchGlory = new TouchOfGlory(components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom));
            components.Add(touchGlory);
        }

        public void LeveledUp(ComponentBag components)
        {
            if(source.Level == 8)
            {
                divPres = new DivinePresence();
                components.Add(divPres);
            }
        }
    }
}