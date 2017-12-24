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

    public class Protection : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel source;
        private ResistantTouch touch;
        private AuraOfProtection aura;
        public Protection(IObjectStore data) : base(data)
        {
            
        }

        public void Initialize(ComponentContainer components)
        {
            source = components.Get<ClassLevel>();
            touch = new ResistantTouch(components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom));
            components.Add(touch);
        }

        public void LeveledUp(ComponentContainer components)
        {
            if(source.Level == 8)
            {
                aura = new AuraOfProtection();
                components.Add(aura);
            }
        }
    }
}