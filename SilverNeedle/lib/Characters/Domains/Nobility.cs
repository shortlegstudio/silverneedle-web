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

    public class Nobility : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel source;
        private InspiringWord inspire;
        public Nobility(IObjectStore data) : base(data)
        {
            
        }

        public void Initialize(ComponentBag components)
        {
            source = components.Get<ClassLevel>();
            inspire = new InspiringWord(components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom));
            components.Add(inspire);
        }

        public void LeveledUp(ComponentBag components)
        {
            if(source.Level == 8)
            {
                var featTokens = components.Get<List<FeatToken>>();
                featTokens.Add(new FeatToken("Leadership"));
            }
        }
    }
}