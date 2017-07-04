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

    public class Madness : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel source;
        private VisionOfMadness vision;
        private AuraOfMadness aura;
        public Madness(IObjectStore data) : base(data)
        {
            
        }

        public void Initialize(ComponentBag components)
        {
            source = components.Get<ClassLevel>();
            vision = new VisionOfMadness(components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom));
            components.Add(vision);
        }

        public void LeveledUp(ComponentBag components)
        {
            if(source.Level == 8)
            {
                aura = new AuraOfMadness();
                components.Add(aura);
            }
        }
    }
}