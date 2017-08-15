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

    
    public class ChooseCombatStyleTests : RequiresDataFiles
    {
        [Fact]
        public void ChooseACombatStyle()
        {
            var character = new CharacterSheet();
            var step = new ChooseCombatStyle();
            step.Process(character, new CharacterBuildStrategy());
            Assert.NotNull(character.Get<CombatStyle>()); 
        }
    }
}