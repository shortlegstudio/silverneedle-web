// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.Personality
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.Personality;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Personalities;

    
    public class SelectQuirksTests : RequiresDataFiles
    {
        [Fact]
        public void SelectsSomeQuirksFromAvailableList()
        {
            var character = CharacterTestTemplates.AverageBob();
            var step = new SelectQuirks();
            step.ExecuteStep(character);
            var quirks = character.Get<Quirks>();
            Assert.NotNull(quirks);
            Assert.NotEmpty(quirks.Items);
        }
    }
}