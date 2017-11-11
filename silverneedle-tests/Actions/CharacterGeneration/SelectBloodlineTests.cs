// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.ClassFeatures
{
    using SilverNeedle.Actions.CharacterGeneration.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;
    using Xunit;

    public class SelectBloodlineTests : RequiresDataFiles
    {
        [Fact]
        public void AssignsBloodlineToCharacter()
        {
            var selector = new SelectBloodline();
            var character = CharacterTestTemplates.AverageBob().WithSkills();
            selector.ExecuteStep(character);
            Assert.NotNull(character.Get<Bloodline>());
        }
    }
}