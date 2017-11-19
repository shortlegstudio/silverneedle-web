// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Domains
{
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;
    public class Earth : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel clericLevel;
        private AcidDart acidDart; 
        private DamageResistance acidResistance;
        public Earth(IObjectStore data) : base(data)
        {
            
        }

        public void Initialize(ComponentBag components)
        {
            clericLevel = components.Get<ClassLevel>();
            this.acidDart = new AcidDart(clericLevel, components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom));
            components.Add(acidDart);
        }

        public void LeveledUp(ComponentBag components)
        {
            var defenseStats = components.Get<DefenseStats>();
            if(clericLevel.Level == 6)
            {
                acidResistance = new DamageResistance(10, "acid");
                defenseStats.AddDamageResistance(acidResistance);
            } 

            if(clericLevel.Level == 12)
            {
                acidResistance.Amount = 20;
            }

            if(clericLevel.Level == 20)
            {
                acidResistance.Amount = 0;
                defenseStats.AddImmunity("acid");
            }
        }
    }
}