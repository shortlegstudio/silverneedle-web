// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;

    public class DivineBondWeapon : SpecialAbility, IComponent
    {
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

        public override string Name
        {
            get
            {
                if(paladinLevel == null)
                    return base.Name;

                return string.Format("Divine Bond (Weapon {0}, {1}/day)", WeaponBonus.ToModifierString(), UsesPerDay);
            }
        }
        private ClassLevel paladinLevel;
        public void Initialize(ComponentBag components)
        {
            this.paladinLevel = components.Get<ClassLevel>();
        }
    }
}