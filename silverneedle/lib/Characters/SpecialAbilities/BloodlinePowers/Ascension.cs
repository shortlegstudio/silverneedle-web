// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;

    public class Ascension : SpecialAbility, IBloodlinePower, IComponent
    {
        public void Initialize(ComponentBag components)
        {
            var defense = components.Get<DefenseStats>();
            var drElectricity = new DamageResistance(10, "electricity");
            var drFire = new DamageResistance(10, "fire");
            defense.AddDamageResistance(drElectricity);
            defense.AddDamageResistance(drFire);
            defense.AddImmunity("acid");
            defense.AddImmunity("cold");
            defense.AddImmunity("petrification");
            var poisonResistance = new ConditionalStatModifier(new ValueStatModifier("saves", 4, "bonus", this.Name), "poison");
            defense.FortitudeSave.AddModifier(poisonResistance);
            defense.ReflexSave.AddModifier(poisonResistance);
            defense.WillSave.AddModifier(poisonResistance);

        }
    }
}