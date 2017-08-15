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

    
    public class UpgradeFavoredEnemyTests
    {
        [Fact]
        public void SelectsAFavoredEnemyAndAddsTwoToTheBonus()
        {
            var ct = new CreatureType("Dragon");
            var favEnemy = new FavoredEnemy(ct);
            var character = new CharacterSheet();
            character.Add(favEnemy);
            var upgrade = new UpgradeFavoredEnemy();
            upgrade.Process(character, new CharacterBuildStrategy());
            Assert.Equal(favEnemy.Bonus(ct), 4);
        }
    }
}