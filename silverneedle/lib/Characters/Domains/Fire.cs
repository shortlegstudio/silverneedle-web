// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Domains
{
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class Fire : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel clericLevel;
        private FireBolt fireBolt;
        private EnergyResistance fireResistance;

        public Fire(IObjectStore data) : base(data)
        {
            
        }
        
          public void Initialize(ComponentContainer components)
        {
            clericLevel = components.Get<ClassLevel>();
            this.fireBolt = new FireBolt(clericLevel, components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom));
            components.Add(fireBolt);
        }

        public void LeveledUp(ComponentContainer components)
        {
            var defenseStats = components.Get<DefenseStats>();
            if(clericLevel.Level == 6)
            {
                fireResistance = new EnergyResistance(10, "fire");
                defenseStats.AddDamageResistance(fireResistance);
            } 

            if(clericLevel.Level == 12)
            {
                fireResistance.Amount = 20;
            }

            if(clericLevel.Level == 20)
            {
                fireResistance.Amount = 0;
                defenseStats.AddImmunity("fire");
            }
        }
    }
}