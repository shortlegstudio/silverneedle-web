// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Utility
{
    using Xunit;
    using SilverNeedle.Utility;

    public class ObjectExtensionTests
    {
        [Fact]
        public void AllowsSettingADefaultIfObjectIsNull()
        {
            object something = null;
            var result = something.Default("Woohoo");
            Assert.Equal("Woohoo", result);
        }

        [Fact]
        public void ReturnsValueOfObjectOnDefaultIfNotNull()
        {
            var something = "Foobar";
            var result = something.Default("Woohoo");
            Assert.Equal("Foobar", result);
        }
    }
}