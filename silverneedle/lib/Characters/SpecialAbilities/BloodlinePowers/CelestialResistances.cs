// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;

    public class CelestialResistances : AbilityDisplayAsName, IBloodlinePower, IComponent, IImprovesWithLevels
    {
        public ComponentContainer Parent { get; set; }
        private EnergyResistance drCold;
        private EnergyResistance drAcid;
        public void Initialize(ComponentContainer components)
        {
            drCold = new EnergyResistance(5, "cold");
            drAcid = new EnergyResistance(5, "acid");
            var defense = components.Get<DefenseStats>();
            defense.AddDamageResistance(drCold);
            defense.AddDamageResistance(drAcid);
        }

        public void LeveledUp(ComponentContainer components)
        {
            var level = components.Get<ClassLevel>();
            if(level.Level == 9)
            {
                drCold.Amount = 10;
                drAcid.Amount = 10;
            }
        }
    }
}