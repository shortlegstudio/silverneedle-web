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

    public class Travel : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel source;
        private AgileFeet agileFeet;
        private DimensionalHop dimenHop;
        public Travel(IObjectStore data) : base(data)
        {
            
        }

        public void Initialize(ComponentContainer components)
        {
            source = components.Get<ClassLevel>();
            agileFeet = new AgileFeet(components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom));
            components.Add(agileFeet);
        }

        public void LeveledUp(ComponentContainer components)
        {
            if(source.Level == 8)
            {
                dimenHop = new DimensionalHop();
                components.Add(dimenHop);
            }
        }
    }
}