// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using System;
    using System.Linq;
    using SilverNeedle.Bestiary;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    public class SelectFavoredEnemy : ICharacterDesignStep, ICharacterFeatureCommand
    {
        private EntityGateway<CreatureType> creatureTypeGateway;

        public SelectFavoredEnemy()
        {
            this.creatureTypeGateway = GatewayProvider.Get<CreatureType>();
        }

        public SelectFavoredEnemy(EntityGateway<CreatureType> ctGateway)
        {
            this.creatureTypeGateway = ctGateway;
        }
        
        public void ExecuteStep(CharacterSheet character)
        {
            Execute(character.Components);
        }

        public void Execute(Utility.ComponentContainer components)
        {
            var favoredEnemy = components.Get<FavoredEnemy>();
            if (favoredEnemy == null)
            {
                favoredEnemy = new FavoredEnemy();
                components.Add(favoredEnemy);
            }
            var type = this.creatureTypeGateway.Where(x => !favoredEnemy.CreatureTypes.Contains(x)).ChooseOne(); 
            favoredEnemy.Add(type);
        }
    }
}