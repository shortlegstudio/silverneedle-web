// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration;
    using SilverNeedle.Characters;
    using SilverNeedle.Utility;

    public class AddToCharacterTests
    {
        [Fact]
        public void InstantiatesTheTypeSpecifiedAndAddsToTheCharacter()
        {
            var yaml = @"---
type: SilverNeedle.Characters.FavoredClassToken";
            var step = new AddToCharacter(yaml.ParseYaml());
            var character = CharacterTestTemplates.AverageBob();
            step.ExecuteStep(character);
            Assert.NotNull(character.Get<FavoredClassToken>());

        }
    }
}