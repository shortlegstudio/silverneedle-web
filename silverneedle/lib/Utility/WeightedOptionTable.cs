// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using System.Linq;

namespace SilverNeedle.Utility
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

        public WeightedOptionTable(IEnumerable<T> list) : this()
        {
            foreach(var item in list)
            {
                var cast = (IWeightedTableObject)item;

                AddEntry(item, cast.Weighting);
            }
        }

        public bool IsEmpty
        {
            get { return table.Count == 0 || maxValue == 1; }
        }

        public IEnumerable<TableEntry> All
        {
            get { return table; }
        }

        public IEnumerable<TableEntry> Enabled
        {
            get { return All.Where(x => x.Disabled == false); }
        }

        public void AddEntry(T option, int weight)
        {
            var entry = new TableEntry();            
            entry.Option = option;
            entry.Weight = weight;
            entry.MinimumValue = maxValue;
            entry.MaximumValue = maxValue + weight - 1;
            maxValue = entry.MaximumValue + 1;
            table.Add(entry);
        }

        public void Disable(T option)
        {
            var entry = FindEntry(option);
            entry.Disabled = true;
            entry.MinimumValue = 0;
            entry.MaximumValue = 0;
            RecalculateTable();
        }

        public void Enable(T option)
        {
            var entry = FindEntry(option);
            entry.Disabled = false;
            RecalculateTable();
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

        public bool HasOption(T option)
        {
            return FindEntry(option) != null;
        }

        public IEnumerable<T> UniqueList()
        {
            var tempTable = Copy(false);
            List<T> list = new List<T>();
            while(!tempTable.IsEmpty) {
                var opt = tempTable.ChooseRandomly();
                tempTable.Disable(opt);
                list.Add(opt);
            }
            return list;
        }

        public WeightedOptionTable<T> Copy(bool includeDisabled = true)
        {
            //Create temp enabled Table
            var tempTable = new WeightedOptionTable<T>();
            foreach(var a in All)
            {
                if(!a.Disabled || includeDisabled) {
                    tempTable.AddEntry(a.Option, a.Weight);
                }
            }
            return tempTable;
        }

        public override string ToString() 
        {
            var entries = string.Join("\n", table.Select(x => x.ToString()));
            return string.Format("-- Weighted Table --\n{0}", entries);
        }

        private TableEntry FindEntry(T option) 
        {
            if(option is String)
            {
                return PerformCaseInsensitiveFind(option.ToString());
            }
            return table.FirstOrDefault(x => option.Equals(x.Option));
        }

        private TableEntry PerformCaseInsensitiveFind(string option)
        {
            return table.FirstOrDefault(x => option.EqualsIgnoreCase(x.Option.ToString()));
        }

        private void RecalculateTable()
        {
            maxValue = 1;
            foreach(var entry in All)
            {
                if (!entry.Disabled)
                {                    
                    entry.MinimumValue = maxValue;
                    entry.MaximumValue = entry.MinimumValue + entry.Weight - 1;
                    maxValue = entry.MaximumValue + 1;
                }
            }
        }

        public class TableEntry
        {
            public int MinimumValue { get; set; }
            public int MaximumValue { get; set; }
            public int Weight { get; set; }

            public T Option { get; set; }

            public bool Disabled { get; set; }

            public override string ToString() {
                return string.Format("Entry: {0} ({1}-{2})[{3}]", Option, MinimumValue, MaximumValue, Weight);
            }
        }
    }
}

