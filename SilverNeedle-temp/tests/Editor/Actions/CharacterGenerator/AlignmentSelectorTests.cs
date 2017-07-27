// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator
{
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator;
    using SilverNeedle.Characters;

    [TestFixture]
    public class AlignmentSelectorTests
    {
        [Fact]
        public void ChoosesAnAlignmentFromPossibleAlignments()
        {
            var selector = new AlignmentSelector();
            var character = new CharacterSheet();
            var strat = new CharacterBuildStrategy();            
            strat.FavoredAlignments.Disable(CharacterAlignment.ChaoticEvil);
            strat.FavoredAlignments.Disable(CharacterAlignment.ChaoticGood);
            strat.FavoredAlignments.Disable(CharacterAlignment.ChaoticNeutral);
            strat.FavoredAlignments.Disable(CharacterAlignment.LawfulEvil);
            strat.FavoredAlignments.Disable(CharacterAlignment.LawfulGood);
            strat.FavoredAlignments.Disable(CharacterAlignment.LawfulNeutral);
            strat.FavoredAlignments.Disable(CharacterAlignment.NeutralEvil);
            strat.FavoredAlignments.Disable(CharacterAlignment.NeutralGood);
            selector.Process(character, strat);
            Assert.That(character.Alignment, Is.EqualTo(CharacterAlignment.Neutral));
        }
    }
}