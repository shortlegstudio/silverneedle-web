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

    public class Healing : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel source;
        private RebukeDeath rebuke;
        private HealerBlessing healBless;
        public Healing(IObjectStore data) : base(data)
        {
            
        }

        public void Initialize(ComponentBag components)
        {
            source = components.Get<ClassLevel>();
            rebuke = new RebukeDeath(components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom));
            components.Add(rebuke);
        }

        public void LeveledUp(ComponentBag components)
        {
            if(source.Level == 6)
            {
                healBless = new HealerBlessing();
                components.Add(healBless);
            }
        }
    }
}