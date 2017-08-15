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

    public class ChooseFavoredEnemy : ICharacterDesignStep
    {
        private EntityGateway<CreatureType> creatureTypeGateway;

        public ChooseFavoredEnemy()
        {
            this.creatureTypeGateway = GatewayProvider.Get<CreatureType>();
        }

        public ChooseFavoredEnemy(EntityGateway<CreatureType> ctGateway)
        {
            this.creatureTypeGateway = ctGateway;
        }
        
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            var favoredEnemy = character.Get<FavoredEnemy>();
            if (favoredEnemy == null)
            {
                favoredEnemy = new FavoredEnemy();
                character.Add(favoredEnemy);
            }
            var type = this.creatureTypeGateway.Where(x => !favoredEnemy.CreatureTypes.Contains(x)).ChooseOne(); 
            favoredEnemy.Add(type);
        }
    }
}