// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Equipment
{
    public class MasterworkArmor : ArmorDecorator
    {
        public MasterworkArmor(IArmor armor) : base(armor)
        {
            this.NamePreface = "Masterwork";
            this.ArmorCheckPenaltyModifier = 1;
            this.AdditionalValue = 150;
        }
    }
}