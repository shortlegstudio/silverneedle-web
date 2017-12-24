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

    public class Weather : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel source;
        private StormBurst stormBurst;
        private LightningLord lightningLord;
        public Weather(IObjectStore data) : base(data)
        {

        }

        public void Initialize(ComponentContainer components)
        {
            source = components.Get<ClassLevel>();
            stormBurst = new StormBurst(components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom));
            components.Add(stormBurst);
        }

        public void LeveledUp(ComponentContainer components)
        {
            if(source.Level == 8)
            {
                lightningLord = new LightningLord();
                components.Add(lightningLord);
            }
        }
    }
}