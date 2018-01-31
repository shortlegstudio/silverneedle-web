// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;

    public class InfernalResistances : AbilityDisplayAsName, IBloodlinePower, IComponent, IImprovesWithLevels
    {
        private EnergyResistance fireResistance;
        private ConditionalStatModifier saveModifier;
        private ClassLevel sorcererLevels;
        public void Initialize(ComponentContainer components)
        {
            sorcererLevels = components.Get<ClassLevel>();
            fireResistance = new EnergyResistance(5, "fire");
            components.Get<DefenseStats>().AddDamageResistance(fireResistance);
            saveModifier = new ConditionalStatModifier
            (
                new DelegateStatModifier("Saves",
                    "bonus",
                    () => { return sorcererLevels.Level >= 9 ? 4 : 2; }
                ), "poison"
            );
            var defense = components.Get<DefenseStats>();
            defense.FortitudeSave.AddModifier(saveModifier);
            defense.ReflexSave.AddModifier(saveModifier);
            defense.WillSave.AddModifier(saveModifier);
        }

        public void LeveledUp(ComponentContainer components)
        {
            if(sorcererLevels.Level == 9)
                fireResistance.Amount = 10;
        }
    }
}