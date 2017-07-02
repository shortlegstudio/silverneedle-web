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

    public class Charm : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel sourceClass;
        private DazingTouch dazingTouch;
        private CharmingSmile charmingSmile;

        public Charm(IObjectStore data) : base(data)
        {
            
        }

        public void Initialize(ComponentBag components)
        {
            sourceClass = components.Get<ClassLevel>();
            this.dazingTouch = new DazingTouch(components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom));
            components.Add(this.dazingTouch);
        }

        public void LeveledUp(ComponentBag components)
        {
            if(sourceClass.Level == 8)
            {
                charmingSmile = new CharmingSmile();
                components.Add(charmingSmile);
            }
        }
    }
}