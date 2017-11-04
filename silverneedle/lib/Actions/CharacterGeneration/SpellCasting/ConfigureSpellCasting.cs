// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.SpellCasting
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;

    public class ConfigureSpellCasting : ICharacterDesignStep
    {
        public void ExecuteStep(CharacterSheet character)
        {
            if(character.Class.HasSpells)
            {
                var cls = character.Get<ClassLevel>();
                var spellcasting = new DivineCasting(cls, cls.Class.Spells.List);
                spellcasting.SpellsKnown = character.Class.Spells.Known;
                spellcasting.SetCastingAbility(character.AbilityScores.GetAbility(character.Class.Spells.Ability));
                character.Add(spellcasting);
            }
        }
    }
}