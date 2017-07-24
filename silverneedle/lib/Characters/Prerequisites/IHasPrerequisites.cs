// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Prerequisites
{
    public interface IHasPrerequisites
    {
        PrerequisiteList Prerequisites { get; }
        bool IsQualified(CharacterSheet character);
    }
}