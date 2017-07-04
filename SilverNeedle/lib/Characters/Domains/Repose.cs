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

    public class Repose : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel source;
        private GentleRest rest;
        private WardAgainstDeath ward;
        public Repose(IObjectStore data) : base(data)
        {
            
        }

        public void Initialize(ComponentBag components)
        {
            source = components.Get<ClassLevel>();
            rest = new GentleRest(components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom));
            components.Add(rest);
        }

        public void LeveledUp(ComponentBag components)
        {
            if(source.Level == 8)
            {
                ward = new WardAgainstDeath();
                components.Add(ward);
            }
        }
    }
}