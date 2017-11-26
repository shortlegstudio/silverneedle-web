// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    public abstract class BloodlineArcana : SpecialAbility
    {
        public abstract string BonusAbility { get; }

        public override string Name 
        {
            get 
            {
                return "{0} ({1})".Formatted(base.Name, this.BonusAbility);
            }
        }
    }
}