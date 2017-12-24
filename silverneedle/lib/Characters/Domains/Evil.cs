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

    public class Evil : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel source;
        private TouchOfEvil touchOfEvil;
        private ScytheOfEvil scytheOfEvil;
        public Evil(IObjectStore data) : base(data)
        {
            
        }

        public void Initialize(ComponentContainer components)
        {
            this.source = components.Get<ClassLevel>();
            touchOfEvil = new TouchOfEvil(components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom));
            components.Add(touchOfEvil);
        }

        public void LeveledUp(ComponentContainer components)
        {
            if(source.Level == 8)
            {
                scytheOfEvil = new ScytheOfEvil(source);
                components.Add(scytheOfEvil);
            }
        }
    }
}