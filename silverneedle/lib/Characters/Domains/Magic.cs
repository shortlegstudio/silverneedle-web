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

    public class Magic : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel source;
        private HandOfTheAcolyte hand;
        private DispellingTouch dispel;
        public Magic(IObjectStore data) : base(data)
        {
            
        }

        public void Initialize(ComponentContainer components)
        {
            source = components.Get<ClassLevel>();
            hand = new HandOfTheAcolyte(components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom));
            components.Add(hand);
        }

        public void LeveledUp(ComponentContainer components)
        {
            if(source.Level == 8)
            {
                dispel = new DispellingTouch(source);
                components.Add(dispel);
            }
        }
    }
}