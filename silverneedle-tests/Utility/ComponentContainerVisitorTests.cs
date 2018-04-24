// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Utility
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Utility;

    public class ComponentContainerVisitorTests
    {
        [Fact]
        public void ComponentContainerVisitorRetrievesAllComponentContainersInTree()
        {
            var cRoot = new ComponentContainer();
            var c1Level1 = new ComponentContainer();
            var c1Level2 = new ComponentContainer();
            var c2Level1 = new ComponentContainer();
            var c3Level1 = new ComponentContainer();

            cRoot.Add(c1Level1);
            cRoot.Add(c1Level2);
            c1Level1.Add(c2Level1);
            c2Level1.Add(c3Level1);

            var vRoot = new ComponentContainer.ComponentContainerVisitor(cRoot);
            var v1 = new ComponentContainer.ComponentContainerVisitor(c1Level1);
            var v3 = new ComponentContainer.ComponentContainerVisitor(c3Level1);


            AssertExtensions.Contains(cRoot, vRoot.GetAllContainers().ToArray());
            AssertExtensions.Contains(c1Level1, vRoot.GetAllContainers().ToArray());
            AssertExtensions.Contains(c1Level2, vRoot.GetAllContainers().ToArray());
            AssertExtensions.Contains(c2Level1, vRoot.GetAllContainers().ToArray());
            AssertExtensions.Contains(c3Level1, vRoot.GetAllContainers().ToArray());

            AssertExtensions.Contains(cRoot, v1.GetAllContainers().ToArray());
            AssertExtensions.Contains(c1Level1, v1.GetAllContainers().ToArray());
            AssertExtensions.Contains(c1Level2, v1.GetAllContainers().ToArray());
            AssertExtensions.Contains(c2Level1, v1.GetAllContainers().ToArray());
            AssertExtensions.Contains(c3Level1, v1.GetAllContainers().ToArray());

            AssertExtensions.Contains(cRoot, v3.GetAllContainers().ToArray());
            AssertExtensions.Contains(c1Level1, v3.GetAllContainers().ToArray());
            AssertExtensions.Contains(c1Level2, v3.GetAllContainers().ToArray());
            AssertExtensions.Contains(c2Level1, v3.GetAllContainers().ToArray());
            AssertExtensions.Contains(c3Level1, v3.GetAllContainers().ToArray());
        }

        [Fact]
        public void ReturnsAComponentContainerOneTimeEvenIfItEndsUpBeingInTwoLists()
        {
            var cRoot = new ComponentContainer();
            var c1Level1 = new ComponentContainer();
            var c2Level1 = new ComponentContainer();

            cRoot.Add(c1Level1);
            c1Level1.Add(c2Level1);
            c2Level1.Add(c1Level1);

            Assert.Equal(3, new ComponentContainer.ComponentContainerVisitor(cRoot).GetAllContainers().Count());
        }
    }
}