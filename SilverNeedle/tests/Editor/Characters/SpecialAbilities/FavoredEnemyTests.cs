// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using NUnit.Framework;
    using SilverNeedle.Beastiary;
    using SilverNeedle.Characters.SpecialAbilities;

    [TestFixture]
    public class FavoredEnemyTests
    {
        [Test]
        public void NameShouldReflectFavoredEnemyAndBonus()
        {
            var creatureType = new CreatureType("Outsider", "Fire");
            var favEnemy = new FavoredEnemy(creatureType);
            Assert.That(favEnemy.Name, Is.EqualTo("Favored Enemy (Outsider (Fire) +2)"));

        }

        [Test]
        public void CanHaveMultipleFavoredEnemies()
        {
            var dragons = new CreatureType("Dragons");
            var rats = new CreatureType("Rats");

            var favEnemy = new FavoredEnemy(dragons);
            favEnemy.Add(rats);
            Assert.That(favEnemy.CreatureTypes, Contains.Item(dragons));
            Assert.That(favEnemy.CreatureTypes, Contains.Item(rats));
            Assert.That(favEnemy.Bonus(rats), Is.EqualTo(2));
            Assert.That(favEnemy.Bonus(dragons), Is.EqualTo(2));
            Assert.That(favEnemy.Name, Is.EqualTo("Favored Enemy (Dragons +2, Rats +2)"));
        }
    }

}