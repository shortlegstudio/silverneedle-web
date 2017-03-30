// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGenerator.SpellCasting
{
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Spells;

    public class PrepareSpells : ICharacterDesignStep
    {
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            if(character.SpellCasting.SpellsKnown == SpellsKnown.None)
                return;

            for(int level = 0; level <= character.SpellCasting.MaxLevel; level++)
            {
                int spellCount = character.SpellCasting.GetSpellsPerDay(level);
                var spells = character.SpellCasting.GetAvailableSpells(level).Choose(spellCount);
                character.SpellCasting.PrepareSpells(level, spells.ToArray());
            }
        }
    }
}