// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    public abstract class BloodlineArcana : IAbility, INameByType
    {
        public abstract string BonusAbility { get; }

        public string DisplayString() 
        {
            return "{0} ({1})".Formatted(this.Name(), this.BonusAbility);
        }
    }
}