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

    public class Strength : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel source;
        private StrengthSurge strengthSurge;
        private MightOfTheGods mightOfGods;
        public Strength(IObjectStore data) : base(data)
        {
            
        }

        public void Initialize(ComponentBag components)
        {
            source = components.Get<ClassLevel>();
            strengthSurge = new StrengthSurge(components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom));
            components.Add(strengthSurge);
        }

        public void LeveledUp(ComponentBag components)
        {
            if(source.Level == 8)
            {
                mightOfGods = new MightOfTheGods();
                components.Add(mightOfGods);
            }
        }
    }
}