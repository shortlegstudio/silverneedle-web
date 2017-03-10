//-----------------------------------------------------------------------
// <copyright file="CollectionExtensions.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Utility;

    /// <summary>
    /// Provides extension methods to collections and enumerable classes
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Chooses a random element out of the array and returns the result
        /// </summary>
        /// <returns>The randomly selected item</returns>
        /// <param name="source">Source array to choose from the array</param>
        /// <typeparam name="T">Type of elements in the array</typeparam>
        public static T ChooseOne<T>(this T[] source) 
        {
            var index = Randomly.Range(0, source.Length);
            return source[index];
        }

        /// <summary>
        /// Chooses a random element out of a IList collection and returns the result
        /// </summary>
        /// <returns>The randomly selected item</returns>
        /// <param name="source">Source list to choose for items</param>
        /// <typeparam name="T">The type of items in the list</typeparam>
        public static T ChooseOne<T>(this IEnumerable<T> source) 
        {
            if (source == null || source.Count() == 0) 
            {
                throw new ArgumentException("Cannot choose from an empty list");
            }

            var index = Randomly.Range(0, source.Count());
            return source.ElementAt(index);
        }

        public static IEnumerable<T> Choose<T>(this IEnumerable<T> source, int count) 
        {
            if (source == null || source.Count() == 0) 
            {
                throw new ArgumentException("Cannot choose from an empty list");
            }
            if(count > source.Count())
            {
                throw new ArgumentException("Not enough items in list to choose from.");
            }

            var results = new List<T>();
            for(int i = 0; i < count; i++)
            {                
                var item = source.Where(x => results.Contains(x) == false).ToList().ChooseOne();    
                results.Add(item);            
            }
            return results;
        }

        public static bool HasChoices<T>(this IEnumerable<T> source)
        {
            return source.Count() > 0;
        }

        public static WeightedOptionTable<T> CreateFlatTable<T>(this IEnumerable<T> source) {
            var table = new WeightedOptionTable<T>();
            foreach(var f in source) {
                table.AddEntry(f, 1);
            }            
            return table;
        }

        /// <summary>
        /// Adds an enumerable list of items to a list
        /// </summary>
        /// <param name="source">List to have items appended to it</param>
        /// <param name="items">Items to append to the list</param>
        /// <typeparam name="T">Type of items in both lists</typeparam>
        public static void Add<T>(this IList<T> source, IEnumerable<T> items) 
        {
            foreach (var i in items) 
            {
                source.Add(i);
            }
        }

        /// <summary>
        /// Selects an option in a dropdown: TODO Move this to more appropriate locations
        /// </summary>
        /// <param name="dropdown">Dropdown with list of options</param>
        /// <param name="value">Value to select in dropdown</param>
        /* public static void SelectOption(this UnityEngine.UI.Dropdown dropdown, string value) 
        {
            for (var i = 0; i < dropdown.options.Count; i++) 
            {
                if (dropdown.options[i].text == value) 
                {
                    dropdown.value = i;
                }
            }
        }
        */
    }
}
