// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions
{
    using SilverNeedle.Characters;

    public interface ICharacterDesignStep
    {
        void ExecuteStep(CharacterSheet character, CharacterStrategy strategy);
    }
}