// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGenerator.SpellCasting
{
    using SilverNeedle.Characters;

    public class SetSpellsPerDay : ICharacterDesignStep
    {
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            ShortLog.Debug("-- SetSpellsPerDay --");
            ShortLog.DebugFormat("CasterLevel: {0}", character.SpellCasting.CasterLevel.ToString());
            int casterLevel = character.SpellCasting.CasterLevel;
            if(casterLevel == 0)
                return;

            int[] spellsPerDay = character.Class.Spells.PerDay[casterLevel];
            for(int index = 0; index < spellsPerDay.Length; index++)
            {
                int bonusSpells = (character.SpellCasting.CastingAbility.TotalModifier - index + 4) / 4;
                // Yeah... except zero level spells
                if (index == 0)
                    bonusSpells = 0;
                character.SpellCasting.SetSpellsPerDay(index, spellsPerDay[index] + bonusSpells);
            }
        }
    }
}