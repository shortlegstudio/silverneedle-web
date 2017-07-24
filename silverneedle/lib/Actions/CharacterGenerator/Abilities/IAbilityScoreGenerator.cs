// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGenerator.Abilities
{
    using System.Collections.Generic;

    public interface IAbilityScoreGenerator
    {
        List<int> GetScores();
    }
}