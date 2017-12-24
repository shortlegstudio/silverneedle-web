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

    public class Sun : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel source;
        private SunBlessing sunBlessing;
        private NimbusOfLight nimbus;
        public Sun(IObjectStore data) : base(data)
        {
            
        }

        public void Initialize(ComponentContainer components)
        {
            source = components.Get<ClassLevel>();
            sunBlessing = new SunBlessing();
            components.Add(new SunBlessing());
        }

        public void LeveledUp(ComponentContainer components)
        {
            if(source.Level == 8)
            {
                nimbus = new NimbusOfLight();
                components.Add(nimbus);
            }
        }
    }
}