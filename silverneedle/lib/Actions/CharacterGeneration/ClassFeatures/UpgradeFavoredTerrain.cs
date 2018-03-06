// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using System;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    
    public class UpgradeFavoredTerrain : ICharacterDesignStep, IFeatureCommand
    {
        public void ExecuteStep(CharacterSheet character)
        {
            Execute(character.Components);
        }

        public void Execute(Utility.ComponentContainer components)
        {
            var fav = components.Get<FavoredTerrain>();
            var upgrade = fav.TerrainTypes.ChooseOne();
            fav.EnhanceBonus(upgrade);
        }
    }
}