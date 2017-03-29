// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGenerator
{
    using SilverNeedle.Characters;

    public class SetSpellsPerDay : ICharacterDesignStep
    {
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            int casterLevel = character.SpellCasting.CasterLevel;
            if(casterLevel == 0)
                return;

            int[] spellsPerDay = character.Class.Spells.PerDay[casterLevel];
            for(int index = 0; index < spellsPerDay.Length; index++)
            {
                character.SpellCasting.SetSpellsPerDay(index, spellsPerDay[index]);
            }
        }
    }
}