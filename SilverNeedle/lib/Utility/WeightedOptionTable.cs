// //-----------------------------------------------------------------------
// // <copyright file="WeightedOptionTable.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

namespace SilverNeedle
{
    /// <summary>
    /// Represents a list of options that each have a different weight to select.
    /// When asked, the weighted option table will select an option from the list
    /// 
    /// This allows for weighted randomness that will return complex objects
    /// </summary>
    public class WeightedOptionTable<T>
    {
        private List<TableEntry> table;
        private int maxValue;

        public WeightedOptionTable()
        {
            table = new List<TableEntry>();
            maxValue = 1;
        }

        public IEnumerable<TableEntry> All()
        {
            return table;
        }

        public void AddEntry(T option, int weight)
        {
            var entry = new TableEntry();
            entry.Option = option;
            entry.MinimumValue = maxValue;
            entry.MaximumValue = maxValue + weight - 1;
            maxValue = entry.MaximumValue + 1;
            table.Add(entry);
        }

        public T GetOption(int value)
        {
            var entry = table.FirstOrDefault(x => x.MinimumValue <= value && x.MaximumValue >= value);
            if (entry == null)
            {
                throw new IndexOutOfRangeException(string.Format("Could not find entry in weighted table with value: {0}", value));
            }
            return entry.Option;
        }

        public T ChooseRandomly()
        {
            var value = Randomly.Range(1, maxValue);
            return GetOption(value);
        }

        public class TableEntry
        {
            public int MinimumValue { get; set; }
            public int MaximumValue { get; set; }
            public T Option { get; set; }
        }
    }
}

