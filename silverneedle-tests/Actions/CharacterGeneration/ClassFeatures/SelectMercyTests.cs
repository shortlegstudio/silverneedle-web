// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.ClassFeatures
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    
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

            var process = new SelectMercy(EntityGateway<Mercy>.LoadFromList(mercies));
            process.Process(character, new CharacterBuildStrategy());
            process.Process(character, new CharacterBuildStrategy());
            var selected = character.Get<Mercies>();
            Assert.Equal(selected.MercyList.Count, 2);
            Assert.Equal(selected.MercyList[0].Level, 3);
            Assert.Equal(selected.MercyList[1].Level, 3);
            Assert.NotEqual(selected.MercyList[0].Name, selected.MercyList[1].Name);
        }
    }
}