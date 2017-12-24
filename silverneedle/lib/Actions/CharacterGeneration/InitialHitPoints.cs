// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration
{
    using SilverNeedle.Characters;
    
    /// <summary>
    /// Hit point generator rolls hitpoints for a character
    /// </summary>
    public class InitialHitPoints : ICharacterDesignStep
    {
        public void ExecuteStep(CharacterSheet character)
        {
            var baseHitpoints = new ValueStatModifier(
                (int)character.Class.HitDice,
                "base-hit-points"
            );
            var hitpoints = character.FindStat(StatNames.HitPoints);
            hitpoints.AddModifier(baseHitpoints);
            hitpoints.AddModifier(character.AbilityScores.GetAbility(AbilityScoreTypes.Constitution).UniversalStatModifier);
        }
    }
}