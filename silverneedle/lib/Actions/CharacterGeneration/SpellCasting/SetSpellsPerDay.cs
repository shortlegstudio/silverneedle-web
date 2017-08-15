// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.SpellCasting
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;

    public class SetSpellsPerDay : ICharacterDesignStep
    {
        public void ExecuteStep(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            if(!character.Contains<SpellCasting>())
                return;
                
            foreach(var spellcasting in character.GetAll<SpellCasting>())
            {
                ShortLog.Debug("-- SetSpellsPerDay --");
                ShortLog.DebugFormat("CasterLevel: {0}", spellcasting.CasterLevel.ToString());
                int casterLevel = spellcasting.CasterLevel;
                if(casterLevel == 0)
                    return;

                int[] spellsPerDay = spellcasting.Class.Class.Spells.PerDay[casterLevel];
                for(int index = 0; index < spellsPerDay.Length; index++)
                {
                    int bonusSpells = (spellcasting.CastingAbility.TotalModifier - index + 4) / 4;
                    // Yeah... except zero level spells
                    if (index == 0)
                        bonusSpells = 0;
                    spellcasting.SetSpellsPerDay(index, spellsPerDay[index] + bonusSpells);
                }
            }
        }
    }
}