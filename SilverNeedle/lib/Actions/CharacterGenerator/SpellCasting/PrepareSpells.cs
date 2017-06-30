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

            var spellCastings = character.GetAll<SpellCasting>();
            foreach(var sc in spellCastings)
            {
                Prepare(sc);
            }
        }

        private void Prepare(SpellCasting spellCasting)
        {
            for(int level = 0; level <= spellCasting.MaxLevel; level++)
            {
                int spellCount = spellCasting.GetSpellsPerDay(level);
                var spells = spellCasting.GetAvailableSpells(level).Choose(spellCount);
                spellCasting.PrepareSpells(level, spells.ToArray());
            }
        }
    }
}