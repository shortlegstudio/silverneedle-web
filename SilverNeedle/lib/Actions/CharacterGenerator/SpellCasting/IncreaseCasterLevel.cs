// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGenerator.SpellCasting
{
    using SilverNeedle.Characters;
    using SilverNeedle.Spells;

    public class IncreaseCasterLevel : ICharacterDesignStep
    {
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            if(character.SpellCasting.SpellsKnown != SpellsKnown.None)
            {
                character.SpellCasting.CasterLevel++;
            }
        }
    }
}