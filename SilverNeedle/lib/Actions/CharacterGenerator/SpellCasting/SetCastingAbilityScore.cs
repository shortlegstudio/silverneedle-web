// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGenerator.SpellCasting
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;

    public class SetCastingAbilityScore : ICharacterDesignStep
    {
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            if(character.Class.HasSpells)
                character.Get<SpellCasting>().SetCastingAbility(character.AbilityScores.GetAbility(character.Class.Spells.Ability));
        }
    }
}