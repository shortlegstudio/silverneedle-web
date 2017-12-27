// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Utility
{
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Utility;
    
    public class ComponentContainerTests
    {
        [Fact]
        public void AddingNullComponentResultsInExceptionBeingThrown()
        {
            var contain = new ComponentContainer();
            Assert.Throws(
                typeof(System.ArgumentNullException),
                () => contain.Add(null)
            );
        }
    }
}