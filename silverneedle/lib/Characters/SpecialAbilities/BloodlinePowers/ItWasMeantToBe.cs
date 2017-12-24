// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;
    public class ItWasMeantToBe : SpecialAbility, IBloodlinePower, IComponent
    {
        private ClassLevel sorcererLevels;
        public void Initialize(ComponentContainer components)
        {
            sorcererLevels = components.Get<ClassLevel>();
        }

        public int UsesPerDay
        {
            get
            {
                return sorcererLevels.Level >= 17 ? 2 : 1;
            }
        }

        public override string Name
        {
            get
            {
                return "{0} ({1}/day)".Formatted(base.Name, UsesPerDay);
            }
        }
    }
}