// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using SilverNeedle.Utility;

namespace Tests.Utility
{
    public class WeightedOptionTableTests
    {
        [Fact]
        public void CanAddOptionsAndItTracksTheRanges()
        {
            var table = new WeightedOptionTable<string>();
            table.AddEntry("Foo", 30);
            table.AddEntry("Bar", 20);
            table.AddEntry("Woo", 3);

            var options = table.All.ToArray();
            Assert.Equal(3, options.Count());
            Assert.Equal(30, options[0].MaximumValue);
            Assert.Equal(50, options[1].MaximumValue);
            Assert.Equal(31, options[1].MinimumValue);
        }

        [Fact]
        public void PullsAnOptionOutBasedOnWeightedValue()
        {
            var table = new WeightedOptionTable<string>();
            table.AddEntry("Foo", 30);
            table.AddEntry("Bar", 20);
            table.AddEntry("Woo", 3);

            Assert.Equal("Foo", table.GetOption(23));
            Assert.Equal("Bar", table.GetOption(31));
            Assert.Equal("Woo", table.GetOption(53));
        }

        [Fact]
        public void ThrowsAnExceptionIfValueDoesNotMeetExpectations()
        {
            var table = new WeightedOptionTable<string>();
            table.AddEntry("Foo", 30);
            table.AddEntry("Bar", 20);
            Assert.Throws(typeof(IndexOutOfRangeException), () => table.GetOption(160));
        }

        [Fact]
        public void EmptyTablesAreFlaggedAsEmpty()
        {
            var table = new WeightedOptionTable<string>();
            Assert.True(table.IsEmpty);
        }

        [Fact]
        public void BecauseStringComparisonsCanBeMessyIfFindingEntryByStringUseCaseInsensitiveIfString()
        {
            var table = new WeightedOptionTable<string>();
            table.AddEntry("Foo", 1);
            table.AddEntry("Bar", 2);

            table.Disable("foo"); 
            Assert.Equal(1, table.Enabled.Count());
            Assert.True(table.HasOption("bar"));
        }

        public void EntriesCanBeDisabledWhichForcesADifferentOptionToBeChosen()
        {
            var table = new WeightedOptionTable<string>();
            table.AddEntry("Foo", 12);
            table.AddEntry("Bar", 100);

            table.Disable("Bar");
            for (int i = 0; i < 1000; i++)
            {
                var result = table.ChooseRandomly();
                Assert.Equal("Foo", result);
            }
        }

        [Fact]
        public void DisabledEntriesCanBeReenabled()
        {
            var table = new WeightedOptionTable<string>();
            table.AddEntry("Foo", 483);
            var entry = table.All.First();
            var originalMax = entry.MaximumValue;
            table.Disable("Foo");
            Assert.True(entry.Disabled);
            table.Enable("Foo");
            Assert.False(entry.Disabled);
            Assert.Equal(originalMax, entry.MaximumValue);
        }

        [Fact]
        public void IfAllOptionsAreDisabledThanFlagAsEmpty()
        {
            var table = new WeightedOptionTable<int>();
            table.AddEntry(1, 1);
            table.Disable(1);
            Assert.True(table.IsEmpty);
        }

        [Fact]
        public void ReturnsTrueIfOptionExists()
        {
            var table = new WeightedOptionTable<string>();
            table.AddEntry("Foo", 1);
            Assert.True(table.HasOption("Foo"));
            Assert.False(table.HasOption("Bar"));
        }

        [Fact]
        public void ReturnsARandomListBasedOnPreferredOptions()
        {
            var table = new WeightedOptionTable<string>();
            table.AddEntry("Foo", 1);
            table.AddEntry("Bar", 1000000);
            var list = table.UniqueList();
            Assert.Equal("Bar", list.First());
            Assert.Equal("Foo", list.Last());
        }

        [Fact]
        public void CanInitializeFromAListOfProperObjects()
        {
            var list = new List<DummyEntry>();
            list.Add(new DummyEntry("Foo", 4));
            list.Add(new DummyEntry("Bar", 2));
            var table = new WeightedOptionTable<DummyEntry>(list);
            Assert.Equal(2, table.All.Count());            
        }
        public class DummyEntry : IWeightedTableObject
        {
            public DummyEntry(string name, int weight)
            {
                Name = name;
                Weighting = weight;
            }

            public string Name { get; set; }
            public int Weighting { get; set; }
        }
    }
}