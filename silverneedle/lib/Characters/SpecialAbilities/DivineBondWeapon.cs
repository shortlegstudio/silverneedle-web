// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;

    public class DivineBondWeapon : IAbility, IComponent
    {
        public ComponentContainer Parent { get; set; }
        public int WeaponBonus 
        { 
            get
            {
                return 1 + (paladinLevel.Level - 5) / 3;
            }
        }
        public int UsesPerDay 
        { 
            get
            {
                return 1 + (paladinLevel.Level - 5) / 4;
            }
        }

        public string DisplayString()
        {
            return string.Format("Divine Bond (Weapon {0}, {1}/day)", WeaponBonus.ToModifierString(), UsesPerDay);
        }
        private ClassLevel paladinLevel;
        public void Initialize(ComponentContainer components)
        {
            this.paladinLevel = components.Get<ClassLevel>();
        }
    }
}