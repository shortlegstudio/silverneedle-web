// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration.SpellCasting
{
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Utility;

    public class SelectNewKnownSpells : ICharacterDesignStep, ICharacterFeatureCommand
    {
        public void ExecuteStep(CharacterSheet character)
        {
            Execute(character.Components);
        }

        public void Execute(ComponentContainer components)
        {
            var spellCasting = components.Get<SpontaneousCasting>();
            for(int spellLevel = 0; spellLevel <= spellCasting.GetHighestSpellLevelKnown(); spellLevel++)
            {
                var spellsToLearn = spellCasting.GetKnownSpellCount(spellLevel) - spellCasting.GetReadySpells(spellLevel).Count();

                if(spellsToLearn > 0)
                {
                    var spells = spellCasting.SpellList.GetSpells(spellLevel, components.GetAll<ISpellCastingRule>())
                        .Where(s => spellCasting.CanLearnSpell(spellLevel, s))
                        .Choose(spellsToLearn);
                    foreach(var s in spells)
                        spellCasting.LearnSpell(s);
                }
            }
        }
    }
}