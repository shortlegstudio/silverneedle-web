// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    public class AberrantArcana : BloodlineArcana
    {
        public override string BonusAbility { get { return bonusAbility; } }
        private string bonusAbility;

        public AberrantArcana()
        {
            bonusAbility = "+50% duration polymorph spells";
        }
    }
}