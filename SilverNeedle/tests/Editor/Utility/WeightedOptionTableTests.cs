// //-----------------------------------------------------------------------
// // <copyright file="WeightedOptionTableTests.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using SilverNeedle;
using SilverNeedle.Utility;

namespace Tests.Utility
{
    [TestFixture]
    public class WeightedOptionTableTests
    {
        [Test]
        public void CanAddOptionsAndItTracksTheRanges()
        {
            var table = new WeightedOptionTable<string>();
            table.AddEntry("Foo", 30);
            table.AddEntry("Bar", 20);
            table.AddEntry("Woo", 3);

            var options = table.All().ToArray();
            Assert.AreEqual(3, options.Count());
            Assert.AreEqual(30, options[0].MaximumValue);
            Assert.AreEqual(50, options[1].MaximumValue);
            Assert.AreEqual(31, options[1].MinimumValue);
        }

        [Test]
        public void PullsAnOptionOutBasedOnWeightedValue()
        {
            var table = new WeightedOptionTable<string>();
            table.AddEntry("Foo", 30);
            table.AddEntry("Bar", 20);
            table.AddEntry("Woo", 3);

            Assert.AreEqual("Foo", table.GetOption(23));
            Assert.AreEqual("Bar", table.GetOption(31));
            Assert.AreEqual("Woo", table.GetOption(53));
        }

        [Test]
        public void ThrowsAnExceptionIfValueDoesNotMeetExpectations()
        {
            var table = new WeightedOptionTable<string>();
            table.AddEntry("Foo", 30);
            table.AddEntry("Bar", 20);
            Assert.Throws(typeof(IndexOutOfRangeException), () => table.GetOption(160));
        }

        [Test]
        public void RandomlySelectsOptionBasedOnWeightedValue()
        {
            var table = new WeightedOptionTable<string>();
            table.AddEntry("Foo", 30);
            table.AddEntry("Bar", 10);

            var foo = 0;
            var bar = 0;
            // Run ten thousand times and it should select FOO a few more times
            for (int i = 0; i < 10000; i++)
            {
                string option = table.ChooseRandomly();
                if (option == "Foo")
                {
                    foo++;
                }
                else
                {
                    bar++;
                }
                    
            }
        }

        [Test]
        public void EmptyTablesAreFlaggedAsEmpty()
        {
            var table = new WeightedOptionTable<string>();
            Assert.IsTrue(table.IsEmpty);
        }

        [Test]
        public void EntriesCanBeDisabledWhichForcesADifferentOptionToBeChosen()
        {
            var table = new WeightedOptionTable<string>();
            table.AddEntry("Foo", 12);
            table.AddEntry("Bar", 100);

            table.Disable("Bar");
            for(int i = 0; i < 1000; i++)
            {
                var result = table.ChooseRandomly();
                Assert.AreEqual("Foo", result);
            }
        }

        [Test]
        public void DisabledEntriesCanBeReenabled()
        {
            var table = new WeightedOptionTable<string>();
            table.AddEntry("Foo", 483);
            var entry = table.All().First();
            var originalMax = entry.MaximumValue;
            table.Disable("Foo");
            Assert.IsTrue(entry.Disabled);
            table.Enable("Foo");
            Assert.IsFalse(entry.Disabled);
            Assert.AreEqual(originalMax, entry.MaximumValue);
        }

        [Test]
        public void IfAllOptionsAreDisabledThanFlagAsEmpty()
        {
            var table = new WeightedOptionTable<int>();
            table.AddEntry(1, 1);
            table.Disable(1);
            Assert.IsTrue(table.IsEmpty);
        }

        [Test]
        public void ReturnsTrueIfOptionExists()
        {
            var table = new WeightedOptionTable<string>();
            table.AddEntry("Foo", 1);
            Assert.IsTrue(table.HasOption("Foo"));
            Assert.IsFalse(table.HasOption("Bar"));
        }

        [Test]
        public void ReturnsARandomListBasedOnPreferredOptions() 
        {
            var table = new WeightedOptionTable<string>();
            table.AddEntry("Foo", 1);
            table.AddEntry("Bar", 1000000);
            var list = table.UniqueList();
            Assert.AreEqual("Bar", list.First());
            Assert.AreEqual("Foo", list.Last());
        }

        [Test]
        public void CanInitializeFromAListOfProperObjects()
        {
            var list = new List<DummyEntry>();
            list.Add(new DummyEntry("Foo", 4));
            list.Add(new DummyEntry("Bar", 2));
            var table = new WeightedOptionTable<DummyEntry>(list);
            Assert.AreEqual(2, table.All().Count());
            
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

