// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration
{
    using SilverNeedle.Dice;
    using SilverNeedle.Characters;
    
    /// <summary>
    /// Hit point generator rolls hitpoints for a character
    /// </summary>
    public class HitPointRoller : ICharacterDesignStep
    {
        public void ExecuteStep(CharacterSheet character)
        {
            var cup = new Cup();
            cup.AddDie(new Die(character.Class.HitDice));
            var roll = cup.Roll();
            var modifier = new ValueStatModifier
            (
                roll,
                "Level up HP"
            );
            var hitpoints = character.FindStat(StatNames.HitPoints);
            hitpoints.AddModifier(modifier);
            hitpoints.AddModifier(character.AbilityScores.GetAbility(AbilityScoreTypes.Constitution).UniversalStatModifier);

        }
    }
}