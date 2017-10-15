// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.Background
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.Background;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Background;
    using SilverNeedle.Lexicon;
    using SilverNeedle.Serialization;

    public class BackgroundStorywriterTests : RequiresDataFiles
    {
        [Fact]
        public void SelectsABackgroundStoryToGenerate()
        {
            var backgroundStoryTemplate = new Descriptor("background-story", new string[] { "{{name}} was born in a shack."});
            var writer = new BackgroundStorywriter(EntityGateway<Descriptor>.LoadWithSingleItem(backgroundStoryTemplate));
            var character = new CharacterSheet();
            character.FirstName = "Foo";
            
            writer.ExecuteStep(character, new CharacterStrategy());
            Assert.Equal("Foo was born in a shack.", character.Get<BackgroundStory>().GetStory());
        }
    }
}