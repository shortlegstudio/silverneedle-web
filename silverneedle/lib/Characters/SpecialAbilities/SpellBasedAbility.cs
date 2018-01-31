// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    public class SpellBasedAbility : IAbility
    {
        private string spellName;
        public int UsesPerDay { get; private set; }

        public SpellBasedAbility(string spellName, int usesPerDay)
        {
            this.spellName = spellName;
            this.UsesPerDay = usesPerDay;
        }

        public string DisplayString()
        {
            return "{0}/day {1}".Formatted(
                this.UsesPerDay,
                this.spellName
            );
        }
    }
}