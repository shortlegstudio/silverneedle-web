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

    
    public class SelectCombatStyleTests : RequiresDataFiles
    {
        [Fact]
        public void SelectACombatStyle()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            var step = new SelectCombatStyle();
            step.ExecuteStep(character);
            Assert.NotNull(character.Get<CombatStyle>()); 
        }
    }
}