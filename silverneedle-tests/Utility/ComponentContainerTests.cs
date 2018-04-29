// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Utility
{
    using System.Linq;
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Serialization;
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

        [Fact]
        public void StatModifiersShouldApplyToAllMatchingStatistics()
        {
            var contain = new ComponentContainer();
            var stat1 = new BasicStat("A Statistic");
            var stat2 = new BasicStat("A Statistic");
            contain.Add(stat1);
            contain.Add(stat2);

            var mod = new ValueStatModifier("A Statistic", 10, "foo");
            contain.Add(mod);
            Assert.Equal(10, stat1.TotalValue);
            Assert.Equal(10, stat2.TotalValue);
        }

        [Fact]
        public void IfStatModifierHasStatisticTypeSetDoNotApplyToUnmatchingTypes()
        {
            var contain = new ComponentContainer();
            var custom = new CustomStatType("Stat");
            var stat = new BasicStat("Stat 2");
            var mod = new ValueStatModifier("%stat%", 10, "foo");
            mod.StatisticType = "Tests.Utility.CustomStatType";

            contain.Add(custom);
            contain.Add(stat);
            contain.Add(mod);
            Assert.Equal(10, custom.TotalValue);
            Assert.Equal(0, stat.TotalValue);
        }

        [Fact(Skip="Cannot implement this until weapon modifiers are managed")]
        public void ThrowsExceptionIfCannotApplyModifierBecauseStatIsNotFound()
        {
            var container = new ComponentContainer();
            var mod = new ValueStatModifier("Stat", 10, "Foo");
            Assert.Throws(typeof(StatisticNotFoundException), () => container.Add(mod));
        }

        [Fact]
        public void IfPropertyHasAddToContainerAttributeThenMakeSureThisValueIsAdded()
        {
            var container = new ComponentContainer();
            var add = new AddToContainerTest();
            container.Add(add);
            Assert.NotNull(container.FindStat("Add Me"));

        }

        [Fact]
        public void CanSerializeOutTheEntitiesInTheContainerAndReloadThem()
        {
            var container = new ComponentContainer();
            container.Add(new CustomStatType("Foo"));
            container.Add(new CustomStatType("Bar"));
            var storage = new YamlObjectStore();
            storage.Serialize(container);
            var newContainer = new ComponentContainer();
            storage.Deserialize(newContainer);
            Assert.NotNull(newContainer.FindStat("Foo"));
            Assert.NotNull(newContainer.FindStat("Bar"));
        }

        [Fact]
        public void ReturnsComponentsFromSubcontainersAsWellWhenSearchingOrGettingAll()
        {
            var container = new ComponentContainer();
            var subcontainer = new ComponentContainer();
            subcontainer.Add(new CustomStatType("Foo"));
            container.Add(subcontainer);
            Assert.NotNull(container.FindStat("Foo"));

        }

        [Fact]
        public void SearchesParentHierarchyForObjectsToo()
        {
            var container = new ComponentContainer();
            var subcontainer = new ComponentContainer();
            container.Add(new CustomStatType("Foo"));
            container.Add(subcontainer);
            Assert.NotNull(subcontainer.FindStat("Foo"));
        }

        [Fact]
        public void AssignsIComponentParent()
        {
            var container = new ComponentContainer();
            var comp = new SimpleComponent();
            container.Add(comp);
            Assert.Equal(container, comp.Parent);
        }

        [Fact]
        public void CallsInitializeOnComponent()
        {
            var container = new ComponentContainer();
            var comp = new SimpleComponent();
            container.Add(comp);
            Assert.Equal("Initialized Call", comp.Name);
        }

        [Fact]
        public void IfEnsureUniqueIsImplementedWithoutReplaceDontAddTheSameObject()
        {
            var container = new ComponentContainer();
            var unique = new UniqueLeaveAlone();
            container.Add(unique);
            container.Add(unique);
            Assert.Equal(1, container.GetAll<UniqueLeaveAlone>().Count());
        }

        [Fact]
        public void IfEnsureUniqueAndReplaceIsSetThenSwapTheComponentOut()
        {
            var container = new ComponentContainer();
            var unique = new UniqueReplace();
            var unique2 = new UniqueReplace();
            container.Add(unique);
            container.Add(unique2);
            Assert.Equal(unique2, container.Get<UniqueReplace>());
            Assert.Equal(1, container.GetAll<UniqueReplace>().Count());
            Assert.False(container.Contains(unique));
        }

    }

    public class UniqueLeaveAlone : IEnsureUniqueComponent
    {
        public bool ReplaceExistingComponent { get { return false; } }

        public bool ComponentUniquenessComparison(object obj)
        {
            return obj is UniqueLeaveAlone;
        }
    }

    public class UniqueReplace : IEnsureUniqueComponent
    {
        public bool ReplaceExistingComponent { get { return true; } }

        public bool ComponentUniquenessComparison(object obj)
        {
            return obj is UniqueReplace;
        }
    }

    public class CustomStatType : BasicStat
    {
        public CustomStatType(string name) : base(name) { }
        public CustomStatType(IObjectStore configure) : base(configure) { }
    }

    public class AddToContainerTest
    {
        private IValueStatistic _addMe = new BasicStat("Add Me");

        [AddToContainer]
        public IValueStatistic Add { get { return _addMe; } }
        
    }

    public class SimpleComponent : IComponent
    {
        public ComponentContainer Parent { get; set; }
        public string Name { get; private set; }
        public void Initialize(ComponentContainer components)
        {
            Name = "Initialized Call";
        }
    }
}