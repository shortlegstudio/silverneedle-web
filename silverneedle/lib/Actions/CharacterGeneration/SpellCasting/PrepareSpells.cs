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
            if(character.Get<ICastingPreparation>() == null)
                return;

            ShortLog.Debug("-- PrepareSpells --");
            var spellCastings = character.GetAll<ICastingPreparation>();
            foreach(var sc in spellCastings)
            {
                Prepare(sc);
            }
        }

        private void Prepare(ICastingPreparation spellCasting)
        {
            for(int level = 0; level <= spellCasting.GetHighestSpellLevelKnown(); level++)
            {
                int spellCount = spellCasting.GetSpellsPerDay(level);
                var spellsAvailable = spellCasting.GetKnownSpells(level).ToArray();
                ShortLog.DebugFormat(
                    "Preparing spells from [Level: {0}, Slots: {1}, Spells: {2}]",
                    level,
                    spellCount,
                    string.Join(", ", spellsAvailable)
                );

                if(spellsAvailable.Length < spellCount)
                    spellCount = spellsAvailable.Length;

                var spells = spellsAvailable.Choose(spellCount); 
                spellCasting.PrepareSpells(level, spells.ToArray());
            }
        }
    }
}