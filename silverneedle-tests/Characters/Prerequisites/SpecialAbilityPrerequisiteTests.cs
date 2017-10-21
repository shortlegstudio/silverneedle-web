// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Prerequisites
{
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Prerequisites;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    
    public class SpecialAbilityPrerequisiteTests {
        [Fact]
        public void SpecialAbilityPrerequisite() 
        {
            var special = new SpecialAbilityPrerequisite("Darkvision");
            var c = new CharacterSheet(CharacterStrategy.Default());
            Assert.False(special.IsQualified(c));
            var darkvision = new MemoryStore();
            darkvision.SetValue("name", "Darkvision");
            c.Add(new Trait(darkvision));
            Assert.True(special.IsQualified(c));
        }
    }
}