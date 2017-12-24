// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;

    public class DeathsGift : SpecialAbility, IBloodlinePower, IComponent, IImprovesWithLevels
    {
        private ClassLevel sorcererLevels;
        private EnergyResistance coldResistance;
        private EnergyResistance nonLethal;
        public void Initialize(ComponentContainer components)
        {
            sorcererLevels = components.Get<ClassLevel>();
            coldResistance = new EnergyResistance(5, "cold");
            nonLethal = new EnergyResistance(5, "- against nonlethal");
            var defense = components.Get<DefenseStats>();
            defense.AddDamageResistance(coldResistance);
            defense.AddDamageResistance(nonLethal);
        }

        public void LeveledUp(ComponentContainer components)
        {
            if(sorcererLevels.Level == 9)
            {
                coldResistance.Amount = 10;
                nonLethal.Amount = 10;
            }
        }
    }
}