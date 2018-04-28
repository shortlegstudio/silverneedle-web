// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;

    public class DeathsGift : AbilityDisplayAsName, IBloodlinePower, IComponent, IImprovesWithLevels
    {
        public ComponentContainer Parent { get; set; }
        private ClassLevel sorcererLevels;
        private EnergyResistance coldResistance;
        private DamageReduction nonLethal;
        public void Initialize(ComponentContainer components)
        {
            sorcererLevels = components.Get<ClassLevel>();
            coldResistance = new EnergyResistance(5, "cold");
            nonLethal = new DamageReduction("- vs. nonlethal", 0);
            nonLethal.AddModifier(new DelegateStatModifier(
                nonLethal.Name,
                "level-up",
                () => { return sorcererLevels.Level >= 9 ? 10 : 5; }
            ));
            components.Add(coldResistance);
            components.Add(nonLethal);
        }

        public void LeveledUp(ComponentContainer components)
        {
            if(sorcererLevels.Level == 9)
            {
                coldResistance.Amount = 10;
            }
        }
    }
}