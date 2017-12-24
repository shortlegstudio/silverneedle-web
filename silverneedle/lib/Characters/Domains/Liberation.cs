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

    public class Liberation : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel source;
        private LiberationMobility liberationMob;
        private FreedomCall freeCall;
        public Liberation(IObjectStore data) : base(data)
        {
            
        }

        public void Initialize(ComponentContainer components)
        {
            source = components.Get<ClassLevel>();
            liberationMob = new LiberationMobility();
            components.Add(liberationMob);
        }

        public void LeveledUp(ComponentContainer components)
        {
            if(source.Level == 8)
            {
                freeCall = new FreedomCall();
                components.Add(freeCall);
            }
        }
    }
}