// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;

    public class Ascension : AbilityDisplayAsName, IBloodlinePower, IComponent
    {
        public void Initialize(ComponentContainer components)
        {
            var defense = components.Get<DefenseStats>();
            var drElectricity = new EnergyResistance(10, "electricity");
            var drFire = new EnergyResistance(10, "fire");
            defense.AddDamageResistance(drElectricity);
            defense.AddDamageResistance(drFire);
            defense.AddImmunity("acid");
            defense.AddImmunity("cold");
            defense.AddImmunity("petrification");
            var poisonResistance = new ConditionalStatModifier(new ValueStatModifier("saves", 4, "bonus"), "poison");
            defense.FortitudeSave.AddModifier(poisonResistance);
            defense.ReflexSave.AddModifier(poisonResistance);
            defense.WillSave.AddModifier(poisonResistance);

        }
    }
}