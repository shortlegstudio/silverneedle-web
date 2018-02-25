// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    public class ProcessFavoredClassTokenTests : RequiresDataFiles
    {
        [Fact]
        public void ChoosesClassCharacterHasIfNotAlreadyChosen()
        {
            var character = CharacterTestTemplates.Cleric();
            character.Add(new FavoredClassToken());

            var step = new ProcessFavoredClassToken();
            step.ExecuteStep(character);
            
            var favored = character.Get<FavoredClass>();
            Assert.True(favored.Qualifies(character.Class));
        }

        [Fact]
        public void IfMultipleTokensAreAvailableTheNextOneShouldChooseSomethingDifferent()
        {
            var classes = new Class[] {
                Class.CreateForTesting("Cleric", SilverNeedle.Dice.DiceSides.d8),
                Class.CreateForTesting("Bard", SilverNeedle.Dice.DiceSides.d6)
            };

            var cleric = CharacterTestTemplates.Cleric();
            cleric.Add(new FavoredClassToken());
            cleric.Add(new FavoredClassToken());

            var step = new ProcessFavoredClassToken(EntityGateway<Class>.LoadFromList(classes));
            step.ExecuteStep(cleric);
            
            var allFavored = cleric.GetAll<FavoredClass>();
            AssertExtensions.EquivalentLists(
                new string[] { "Cleric", "Bard" },
                allFavored.Select(x => x.ClassName)
            );


        }
    }
}