// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Domains
{
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;
    public class Water : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel clericLevel;
        private Icicle icicle;
        private DamageResistance coldResistance;

        public Water(IObjectStore data) : base(data)
        {
            
        }

        public void Initialize(ComponentBag components)
        {
            clericLevel = components.Get<ClassLevel>();
            this.icicle = new Icicle(clericLevel, components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom));
            components.Add(this.icicle);
        }

        public void LeveledUp(ComponentBag components)
        {
            var defenseStats = components.Get<DefenseStats>();
            if(clericLevel.Level == 6)
            {
                coldResistance = new DamageResistance(10, "cold");
                defenseStats.AddDamageResistance(coldResistance);
            } 

            if(clericLevel.Level == 12)
            {
                coldResistance.Amount = 20;
            }

            if(clericLevel.Level == 20)
            {
                coldResistance.Amount = 0;
                defenseStats.AddImmunity("cold");
            }
        }
    }
}