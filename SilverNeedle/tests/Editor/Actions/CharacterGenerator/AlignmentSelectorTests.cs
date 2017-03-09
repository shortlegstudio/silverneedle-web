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
        [Test]
        public void ChoosesAnAlignmentFromPossibleAlignments()
        {
            var selector = new AlignmentSelector();
            var character = new CharacterSheet();
            var strat = new CharacterBuildStrategy();            
            selector.ProcessFirstLevel(character, strat);
            Assert.Ignore();
        }
    }
}