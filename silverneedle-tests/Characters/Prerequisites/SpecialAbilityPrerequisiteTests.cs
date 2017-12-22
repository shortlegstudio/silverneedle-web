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
    using Moq;

    
    public class SpecialAbilityPrerequisiteTests {
        [Fact]
        public void SpecialAbilityPrerequisite() 
        {
            var special = new SpecialAbilityPrerequisite("Darkvision");
            var c = CharacterTestTemplates.AverageBob();
            Assert.False(special.IsQualified(c));
            var mock = new Mock<ITrait>();
            mock.SetupGet(x => x.Name).Returns("Darkvision");
            c.Add(mock.Object);
            Assert.True(special.IsQualified(c));
        }
    }
}