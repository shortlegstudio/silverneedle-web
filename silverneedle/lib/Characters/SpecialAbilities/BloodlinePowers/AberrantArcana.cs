// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    public class AberrantArcana : SpecialAbility, IBloodlineArcana
    {
        public string BonusAbility { get; private set; }

        public AberrantArcana()
        {
            BonusAbility = "+50% duration polymorph spells";
        }

        public override string Name 
        {
            get 
            {
                return "{0} ({1})".Formatted(base.Name, this.BonusAbility);
            }
        }
    }
}