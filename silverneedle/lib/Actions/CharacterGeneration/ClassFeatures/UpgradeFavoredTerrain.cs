// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using System;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    
    public class UpgradeFavoredTerrain : ICharacterDesignStep
    {
        public void ExecuteStep(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            var fav = character.Get<FavoredTerrain>();
            var upgrade = fav.TerrainTypes.ChooseOne();
            fav.EnhanceBonus(upgrade);
        }
    }
}