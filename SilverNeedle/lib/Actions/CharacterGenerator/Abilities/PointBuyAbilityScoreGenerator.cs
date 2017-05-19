// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGenerator.Abilities
{
    using System.Collections.Generic;
    using SilverNeedle.Characters;

    public class PointBuyAbilityScoreGenerator : IAbilityScoreGenerator
    {
        AbilityScoreCosts pointCosts;

        public PointBuyAbilityScoreGenerator()
        {
            pointCosts = GatewayProvider.Find<AbilityScoreCosts>("standard");
        }

        public List<int> GetScores()
        {
            return null;
        }
    }
}