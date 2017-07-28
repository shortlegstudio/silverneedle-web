// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Bestiary;
    using SilverNeedle.Characters.SpecialAbilities;

    
    public class FavoredEnemyTests
    {
        [Fact]
        public void NameShouldReflectFavoredEnemyAndBonus()
        {
            var creatureType = new CreatureType("Outsider", "Fire");
            var favEnemy = new FavoredEnemy(creatureType);
            Assert.Equal(favEnemy.Name, "Favored Enemy (Outsider (Fire) +2)");

        }

        [Fact]
        public void CanHaveMultipleFavoredEnemies()
        {
            var dragons = new CreatureType("Dragons");
            var rats = new CreatureType("Rats");

            var favEnemy = new FavoredEnemy(dragons);
            favEnemy.Add(rats);
            Assert.Contains(dragons, favEnemy.CreatureTypes);
            Assert.Contains(rats, favEnemy.CreatureTypes);
            Assert.Equal(favEnemy.Bonus(rats), 2);
            Assert.Equal(favEnemy.Bonus(dragons), 2);
            Assert.Equal(favEnemy.Name, "Favored Enemy (Dragons +2, Rats +2)");
        }
    }

}