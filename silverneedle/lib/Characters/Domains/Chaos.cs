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

    public class Chaos : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel sourceClass;
        private TouchOfChaos touchOfChaos;
        private ChaosBlade chaosBlade;

        public Chaos(IObjectStore data) : base(data)
        {
            
        }

        public void Initialize(ComponentBag components)
        {
            sourceClass = components.Get<ClassLevel>();
            this.touchOfChaos = new TouchOfChaos(components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom));
            components.Add(touchOfChaos);
        }

        public void LeveledUp(ComponentBag components)
        {
            if(sourceClass.Level == 8)
            {
                chaosBlade = new ChaosBlade(sourceClass);
                components.Add(chaosBlade);
            }
        }
    }
}