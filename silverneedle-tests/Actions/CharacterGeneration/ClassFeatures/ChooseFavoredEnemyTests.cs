// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.ClassFeatures
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.ClassFeatures;
    using SilverNeedle.Bestiary;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    
    public class ChooseFavoredEnemyTests
    {
        [Fact]
        public void ChooseFromFavoredEnemyList()
        {
            var aberration = new CreatureType("Aberration"); 
            var ctGateway = EntityGateway<CreatureType>.LoadWithSingleItem(aberration);
            var configureStep = new ChooseFavoredEnemy(ctGateway);
            var character = new CharacterSheet(CharacterStrategy.Default());
            configureStep.ExecuteStep(character, new CharacterStrategy());
            
            var favEnemy = character.Get<FavoredEnemy>();
            Assert.Contains(aberration, favEnemy.CreatureTypes);
        }

        [Fact]
        public void AddingASecondEnemyAppendsADifferentOneToTheList()
        {
            var aberration = new CreatureType("Aberration"); 
            var dragon = new CreatureType("Dragon"); 
            var creatureTypes = new CreatureType[] { aberration, dragon }; 
            var ctGateway = EntityGateway<CreatureType>.LoadFromList(creatureTypes);
            var configureStep = new ChooseFavoredEnemy(ctGateway);
            var character = new CharacterSheet(CharacterStrategy.Default());
            configureStep.ExecuteStep(character, new CharacterStrategy());
            configureStep.ExecuteStep(character, new CharacterStrategy());
            
            var favEnemy = character.Get<FavoredEnemy>();
            Assert.Contains(aberration, favEnemy.CreatureTypes);
            Assert.Contains(dragon, favEnemy.CreatureTypes);
        }
    }
}