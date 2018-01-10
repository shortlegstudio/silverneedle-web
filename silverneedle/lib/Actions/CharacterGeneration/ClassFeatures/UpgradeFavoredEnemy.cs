// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using System;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    
    public class UpgradeFavoredEnemy : ICharacterDesignStep, ICharacterFeatureCommand
    {
        public void ExecuteStep(CharacterSheet character)
        {
            Execute(character.Components);
        }

        public void Execute(Utility.ComponentContainer components)
        {
            //TODO: Can be rewritten as stats that get a bonus....
            var favEnemy = components.Get<FavoredEnemy>();
            var upgrade = favEnemy.CreatureTypes.ChooseOne();
            favEnemy.EnhanceBonus(upgrade);
        }
    }
}