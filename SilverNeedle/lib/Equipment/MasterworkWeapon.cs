// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Equipment
{
    public class MasterworkWeapon : WeaponDecorator
    {
        public MasterworkWeapon(IWeapon reference) : base(reference)
        {

        }

        public override int AttackModifier
        {
            get { return 1 + base.AttackModifier; }
        }

        public override int Value 
        {
            get 
            { 
                if(this.Group == WeaponGroup.Double)
                    return 60000 + base.Value; 
                return 30000 + base.Value; 
            }
        }

        public override string Name
        {
            get { return "Masterwork " + base.Name; }
        }
    }
}