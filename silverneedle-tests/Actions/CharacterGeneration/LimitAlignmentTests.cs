// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    
    public class LimitAlignmentTests
    {
        [Fact]
        public void RemovesAlignmentsFromStrategyIfDenyIsSet()
        {

            var strategy = new CharacterStrategy();
            var data = new MemoryStore();
            data.SetValue("deny", new string[] { "LawfulEvil" , "LawfulNeutral", "LawfulGood" });
            var step = new LimitAlignment(data);
            step.ExecuteStep(new CharacterSheet(), strategy);
            var enabled = strategy.FavoredAlignments.Enabled.Select(x => x.Option);
            Assert.DoesNotContain(CharacterAlignment.LawfulEvil, enabled); 
            Assert.DoesNotContain(CharacterAlignment.LawfulGood, enabled); 
            Assert.DoesNotContain(CharacterAlignment.LawfulNeutral, enabled); 
        }
    }
}