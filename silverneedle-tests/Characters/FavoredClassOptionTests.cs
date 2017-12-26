// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class FavoredClassOptionTests
    {
        [Fact]
        public void LoadsAndGeneratorsAModifierBasedOnOption()
        {
            var yaml = @"---
type: SilverNeedle.ValueStatModifier
name: Hit Points
modifier: 1
modifier-type: favored-class
";
            var option = new FavoredClassOption(yaml.ParseYaml());
            var mod = option.CreateModifier();
            Assert.Equal("Hit Points", mod.StatisticName);
            Assert.Equal(1, mod.Modifier);
            Assert.Equal("favored-class", mod.Type);
        }
    }
}