// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.SpellCasting
{
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Spells;

    public class PrepareSpells : ICharacterDesignStep
    {
        public void ExecuteStep(CharacterSheet character)
        {
            if(character.Get<ISpellCasting>() == null)
                return;

            ShortLog.Debug("-- PrepareSpells --");
            var spellCastings = character.GetAll<ISpellCasting>();
            foreach(var sc in spellCastings)
            {
                Prepare(sc);
            }
        }

        private void Prepare(ISpellCasting spellCasting)
        {
            ShortLog.DebugFormat("Preparing spells for list: {0}", spellCasting.ToString());
            for(int level = 0; level <= spellCasting.MaxLevel; level++)
            {
                int spellCount = spellCasting.GetSpellsPerDay(level);
                var spells = spellCasting.GetAvailableSpells(level).Choose(spellCount);
                spellCasting.PrepareSpells(level, spells.ToArray());
            }
        }
    }
}