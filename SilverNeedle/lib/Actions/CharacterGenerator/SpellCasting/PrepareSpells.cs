// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGenerator.SpellCasting
{
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Spells;

    public class PrepareSpells : ICharacterDesignStep
    {
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            if(character.Get<SpellCasting>() == null)
                return;

            for(int level = 0; level <= character.Get<SpellCasting>().MaxLevel; level++)
            {
                int spellCount = character.Get<SpellCasting>().GetSpellsPerDay(level);
                var spells = character.Get<SpellCasting>().GetAvailableSpells(level).Choose(spellCount);
                character.Get<SpellCasting>().PrepareSpells(level, spells.ToArray());
            }
        }
    }
}