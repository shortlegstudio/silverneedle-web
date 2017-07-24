// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Domains
{
    using System;
    using System.Collections.Generic;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class Darkness : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel source;
        private TouchOfDarkness touchOfDarkness;
        private EyesOfDarkness eyesOfDarkness;
        public Darkness(IObjectStore data) : base(data)
        {
            
        }

        public void Initialize(ComponentBag components)
        {
            source = components.Get<ClassLevel>();
            touchOfDarkness = new TouchOfDarkness(components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom));
            components.Add(touchOfDarkness);
            components.Get<List<FeatToken>>().Add(new FeatToken("Blind-Fight"));
        }

        public void LeveledUp(ComponentBag components)
        {
            if(source.Level == 8)
            {
                eyesOfDarkness = new EyesOfDarkness();
                components.Add(eyesOfDarkness);
            }
        }
    }
}