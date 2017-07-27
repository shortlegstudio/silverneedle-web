// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator
{
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    [TestFixture]
    public class LimitAlignmentTests
    {
        [Fact]
        public void RemovesAlignmentsFromStrategyIfDenyIsSet()
        {

            var strategy = new CharacterBuildStrategy();
            var data = new MemoryStore();
            data.SetValue("deny", "LawfulEvil, LawfulNeutral, LawfulGood");
            var step = new LimitAlignment(data);
            step.Process(new CharacterSheet(), strategy);
            var enabled = strategy.FavoredAlignments.Enabled.Select(x => x.Option);
            Assert.That(enabled, Does.Not.Contains(CharacterAlignment.LawfulEvil)); 
            Assert.That(enabled, Does.Not.Contains(CharacterAlignment.LawfulGood)); 
            Assert.That(enabled, Does.Not.Contains(CharacterAlignment.LawfulNeutral)); 
        }
    }
}