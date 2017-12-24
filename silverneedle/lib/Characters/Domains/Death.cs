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

    public class Death : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel source;
        private BleedingTouch bleedingTouch;
        private DeathEmbrace deathEmbrace;

        public Death(IObjectStore data) : base(data)
        {
            
        }

        public void Initialize(ComponentContainer components)
        {
            this.source = components.Get<ClassLevel>();
            bleedingTouch = new BleedingTouch(components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom));
            components.Add(bleedingTouch);
        }

        public void LeveledUp(ComponentContainer components)
        {
            if(source.Level == 8)
            {
                deathEmbrace = new DeathEmbrace();
                components.Add(deathEmbrace);
            }
        }
    }
}