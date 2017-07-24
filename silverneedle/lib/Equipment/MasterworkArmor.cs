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
        }

        public override string Name
        {
            get
            {
                return string.Format("Masterwork {0}", base.Name);
            }
        }

        public override int ArmorCheckPenalty
        {
            get
            {
                // Reduce penalty by one
                return base.ArmorCheckPenalty + 1;
            }
        }

        public override int Value
        {
            get
            {
                return base.Value + 15000;
            }
        }
    }
}