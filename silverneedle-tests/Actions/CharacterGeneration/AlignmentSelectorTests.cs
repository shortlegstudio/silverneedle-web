// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration;
    using SilverNeedle.Characters;

    public class AlignmentSelectorTests
    {
        [Fact]
        public void ChoosesAnAlignmentFromPossibleAlignments()
        {
            var selector = new AlignmentSelector();
            var character = new CharacterSheet(CharacterStrategy.Default());
            var strat = new CharacterStrategy();            
            strat.FavoredAlignments.Disable(CharacterAlignment.ChaoticEvil);
            strat.FavoredAlignments.Disable(CharacterAlignment.ChaoticGood);
            strat.FavoredAlignments.Disable(CharacterAlignment.ChaoticNeutral);
            strat.FavoredAlignments.Disable(CharacterAlignment.LawfulEvil);
            strat.FavoredAlignments.Disable(CharacterAlignment.LawfulGood);
            strat.FavoredAlignments.Disable(CharacterAlignment.LawfulNeutral);
            strat.FavoredAlignments.Disable(CharacterAlignment.NeutralEvil);
            strat.FavoredAlignments.Disable(CharacterAlignment.NeutralGood);
            selector.ExecuteStep(character, strat);
            Assert.Equal(character.Alignment, CharacterAlignment.Neutral);
        }
    }
}