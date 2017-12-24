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

    public class Destruction : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel source;
        private DestructiveSmite smite;
        private DestructiveAura aura;
        public Destruction(IObjectStore data) : base(data)
        {
            
        }

        public void Initialize(ComponentContainer components)
        {
            source = components.Get<ClassLevel>();
            smite = new DestructiveSmite(components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom));
            components.Add(smite);
        }

        public void LeveledUp(ComponentContainer components)
        {
            if(source.Level == 8)
            {
                aura = new DestructiveAura();
                components.Add(aura);
            }
        }
    }
}