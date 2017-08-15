// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using System;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    
    public class UpgradeFavoredEnemy : ICharacterDesignStep
    {
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            var favEnemy = character.Get<FavoredEnemy>();
            var upgrade = favEnemy.CreatureTypes.ChooseOne();
            favEnemy.EnhanceBonus(upgrade);
        }
    }
}