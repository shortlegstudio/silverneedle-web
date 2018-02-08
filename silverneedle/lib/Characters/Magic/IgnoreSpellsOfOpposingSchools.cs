// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.Magic
{
    using System.Linq;
    using Spells;
    using Utility;

    public class IgnoreSpellsOfOpposingSchools : ISpellCastingRule, IComponent
    {
        private WizardCasting _casting;
        public bool CanCastSpell(Spell spell)
        {
            return !(_casting.OppositionSchools.Any(x => x.Name.EqualsIgnoreCase(spell.School)));
        }

        public void Initialize(ComponentContainer components)
        {
            _casting = components.Get<WizardCasting>();
        }
    }
}