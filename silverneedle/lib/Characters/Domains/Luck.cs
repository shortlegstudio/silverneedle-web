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

    public class Luck : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel source;
        private BitOfLuck bitOfLuck;
        private GoodFortune goodFortune;
        public Luck(IObjectStore data) : base(data)
        {
            
        }

        public void Initialize(ComponentContainer components)
        {
            source = components.Get<ClassLevel>();
            bitOfLuck = new BitOfLuck(components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom));
            components.Add(bitOfLuck);
        }

        public void LeveledUp(ComponentContainer components)
        {
            if(source.Level == 6)
            {
                goodFortune = new GoodFortune();
                components.Add(goodFortune);
            }
        }
    }
}