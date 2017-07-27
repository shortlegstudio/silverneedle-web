// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.ClassFeatures
{
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    [TestFixture]
    public class SelectMercyTests
    {
        [Fact]
        public void SelectsAUniqueMercyFromAvailableLevelAndAddsToMerciesAbility()
        {
            var mercies = new Mercy[] {
                new Mercy("Mercy 1", 3),
                new Mercy("Mercy 2", 3),
                new Mercy("Mercy 3", 6),
                new Mercy("Mercy 4", 6)
            };
            var character = new CharacterSheet();
            var paladin = new Class();
            paladin.Name = "Paladin";
            character.SetClass(paladin);
            character.SetLevel(5);

            var process = new SelectMercy(new EntityGateway<Mercy>(mercies));
            process.Process(character, new CharacterBuildStrategy());
            process.Process(character, new CharacterBuildStrategy());
            var selected = character.Get<Mercies>();
            Assert.That(selected.MercyList.Count, Is.EqualTo(2));
            Assert.That(selected.MercyList[0].Level, Is.EqualTo(3));
            Assert.That(selected.MercyList[1].Level, Is.EqualTo(3));
            Assert.That(selected.MercyList[0].Name, Is.Not.EqualTo(selected.MercyList[1].Name));
        }
    }
}