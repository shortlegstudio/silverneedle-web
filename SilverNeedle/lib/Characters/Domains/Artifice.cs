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

    public class Artifice : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel sourceClass;
        private ArtificerTouch artificerTouch;
        private DancingWeapons danceWeapons;

        public Artifice(IObjectStore data) : base(data)
        {
            
        }

        public void Initialize(ComponentBag components)
        {
            sourceClass = components.Get<ClassLevel>();
            artificerTouch = new ArtificerTouch(components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom));
            components.Add(artificerTouch);
        }

        public void LeveledUp(ComponentBag components)
        {
            if(sourceClass.Level == 8)
            {
                danceWeapons = new DancingWeapons(sourceClass);
                components.Add(danceWeapons);
            }
        }
    }
}