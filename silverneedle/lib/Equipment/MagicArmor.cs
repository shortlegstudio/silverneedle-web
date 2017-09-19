// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Equipment
{
    using SilverNeedle.Treasure;
    using SilverNeedle.Utility;
    using SilverNeedle.Serialization;
    public class MagicArmor : ArmorDecorator
    {
        public int MagicBonus { get; set; }
        public string MagicValue { get; set; }
        public MagicArmor(IArmor armor, int bonus) : base(armor)
        {
            this.MagicBonus = bonus;
            this.MagicValue = GatewayProvider.Find<MagicArmorValue>(MagicBonus.ToString()).Value;
        }

        public override string Name
        {
            get 
            {
                return string.Format("{0} {1}", base.Name, this.MagicBonus.ToModifierString());
            }
        }
        public override int ArmorClass
        {
            get
            {
                return base.ArmorClass + this.MagicBonus;
            }
        }

        public override int Value
        {
            get
            {
                return base.Value + MagicValue.ToCoinValue();
            }
        }
        
        public override int ArmorCheckPenalty
        {
            get
            {
                return base.ArmorCheckPenalty + 1;
            }
        }
    }
}