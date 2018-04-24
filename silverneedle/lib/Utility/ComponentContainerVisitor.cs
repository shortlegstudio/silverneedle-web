// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Utility
{
    using System.Collections.Generic;
    using System.Linq;

    public partial class ComponentContainer
    {

        public class ComponentContainerVisitor
        {
            ComponentContainer startContainer; 
            public ComponentContainerVisitor(ComponentContainer container)
            {
                startContainer = container;
            }


            public IEnumerable<ComponentContainer> GetAllContainers()
            {
                var root = GetRoot(startContainer);
                var uniqueResultList = new List<ComponentContainer>() { root };
                FindContainers(root, uniqueResultList);
                return uniqueResultList;
            }


            private ComponentContainer GetRoot(ComponentContainer container)
            {
                if(container.Parent != null)
                {
                    return GetRoot(container.Parent);
                }

                return container;
            }

            private void FindContainers(ComponentContainer container, IList<ComponentContainer> results)
            {
                var thisLevel = container.components
                    .OfType<ComponentContainer>()
                    .Where(x => !results.Contains(x))
                    .ToList();

                results.Add(thisLevel);

                foreach(var child in thisLevel)
                {
                    FindContainers(child, results);
                }
            }
        }
    }
}