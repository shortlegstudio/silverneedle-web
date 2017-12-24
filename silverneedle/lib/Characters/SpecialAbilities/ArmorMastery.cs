// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;
    public class ArmorMastery : SpecialAbility, IComponent
    {
        public DamageResistance DamageResistance { get; private set; }
        public ArmorMastery(int amount, string damageType)
        {
            this.DamageResistance = new DamageResistance(amount, damageType);
            this.Name = "Armor Mastery";
        }   

        public void Initialize(ComponentContainer components)
        {
            components.Get<DefenseStats>().AddDamageResistance(this.DamageResistance);
        }

    }
}