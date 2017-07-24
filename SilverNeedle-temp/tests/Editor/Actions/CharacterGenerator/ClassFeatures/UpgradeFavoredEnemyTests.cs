// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.ClassFeatures
{
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.ClassFeatures;
    using SilverNeedle.Bestiary;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    [TestFixture]
    public class UpgradeFavoredEnemyTests
    {
        [Test]
        public void SelectsAFavoredEnemyAndAddsTwoToTheBonus()
        {
            var ct = new CreatureType("Dragon");
            var favEnemy = new FavoredEnemy(ct);
            var character = new CharacterSheet();
            character.Add(favEnemy);
            var upgrade = new UpgradeFavoredEnemy();
            upgrade.Process(character, new CharacterBuildStrategy());
            Assert.That(favEnemy.Bonus(ct), Is.EqualTo(4));
        }
    }
}