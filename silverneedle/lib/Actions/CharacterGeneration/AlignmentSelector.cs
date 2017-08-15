// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using System;
using SilverNeedle.Characters;

namespace SilverNeedle.Actions.CharacterGeneration
{
    public class AlignmentSelector : ICharacterDesignStep
    {
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            character.Alignment = strategy.FavoredAlignments.ChooseRandomly();
        }
    }
}