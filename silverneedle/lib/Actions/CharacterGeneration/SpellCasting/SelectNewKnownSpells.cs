// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration.SpellCasting
{
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;
    public class SelectNewKnownSpells : ICharacterDesignStep
    {
        public void ExecuteStep(CharacterSheet character)
        {
            var spellCasting = character.Get<SpontaneousCasting>();
            for(int spellLevel = 0; spellLevel <= spellCasting.GetHighestSpellLevelKnown(); spellLevel++)
            {
                var spellsToLearn = spellCasting.GetKnownSpellCount(spellLevel) - spellCasting.GetReadySpells(spellLevel).Count();

                if(spellsToLearn > 0)
                {
                    SelectAdditionalSpells(spellCasting, spellLevel, spellsToLearn);
                }
            }
        }

        private void SelectAdditionalSpells(SpontaneousCasting casting, int spellLevel, int numberOfSpells)
        {
            var spells = casting.SpellList.Levels[spellLevel].Choose(numberOfSpells);
            foreach(var s in spells)
                casting.LearnSpell(s);
        }
    }
}