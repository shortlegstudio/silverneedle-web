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

    public class Community : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel source;
        private CalmingTouch calmTouch;
        private Unity unity;

        public Community(IObjectStore data) : base(data)
        {

        }

        public void Initialize(ComponentBag components)
        {
            this.source = components.Get<ClassLevel>();
            calmTouch = new CalmingTouch(components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom));
            components.Add(calmTouch);
        }

        public void LeveledUp(ComponentBag components)
        {
            if(source.Level == 8)
            {
                unity = new Unity(source);
                components.Add(unity);
            }
        }
    }
}