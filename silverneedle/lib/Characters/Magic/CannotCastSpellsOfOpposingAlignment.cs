// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Magic
{
    using System.Linq;
    using SilverNeedle.Spells;
    using SilverNeedle.Utility;

    public class CannotCastSpellsOfOpposingAlignment : ISpellCastingRule, IComponent
    {
        private ComponentBag components;
        public bool CanCastSpell(Spell spell)
        {
            var alignment = components.Get<CharacterAlignment>();
            var invalidDescs = GetInvalidDescriptors(alignment);
            if(invalidDescs.Length == 0)
                return true;
            
            var result = !spell.Descriptors.Any(desc => invalidDescs.Contains(desc));
            if(!result)
            {
                ShortLog.DebugFormat("Cannot cast {0} because it is of the wrong alignment. Character is {1}", spell.Name, alignment.ToString());
            }
            return result;
        }

        public void Initialize(ComponentBag components)
        {
            this.components = components;
            this.components.Added += this.WatchForSpellCasting;
        }

        public void WatchForSpellCasting(object source, ComponentBagEvent args)
        {
            //TODO: Change this so that instead of an event which is tricky, that
            //  instead the spellcasting class grabs all rules that are out there
            var spellcasting = args.Component as SpellCasting;
            if(spellcasting != null)
            {
                spellcasting.AddRule(this);
            }
        }

        private string[] GetInvalidDescriptors(CharacterAlignment alignment)
        {
            switch(alignment)
            {
                case CharacterAlignment.ChaoticEvil:
                    return new string[] { "lawful", "good"};
                case CharacterAlignment.ChaoticGood:
                    return new string[] { "lawful", "evil" };
                case CharacterAlignment.ChaoticNeutral:
                    return new string[] { "lawful" };
                case CharacterAlignment.LawfulEvil:
                    return new string[] { "chaotic", "good" };
                case CharacterAlignment.LawfulGood:
                    return new string[] { "chaotic", "evil" };
                case CharacterAlignment.LawfulNeutral:
                    return new string[] { "chaotic" };
                case CharacterAlignment.NeutralEvil:
                    return new string[] { "good" };
                case CharacterAlignment.NeutralGood:
                    return new string[] { "evil " };
            }
            return new string[] { };
        }
    }
}