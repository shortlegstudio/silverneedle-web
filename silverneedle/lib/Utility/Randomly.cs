// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle
{
    //using UnityEngine; //TODO: Remove UnityEngine Reference

    /// <summary>
    /// Provides random number functionality in a consistent way that allows 
    /// unit tests and Unity to run smoothly. Basically just abstracts away
    /// the various behavior
    /// </summary>
    public class Randomly
    {
        /// <summary>
        /// The System Random number generator instance
        /// </summary>
        private static System.Random systemRandom;
        
        // TODO: Allow configuring custom randomizers

        /// <summary>
        /// Initializes static members of the <see cref="SilverNeedle.Randomly"/> class.
        /// </summary>
        static Randomly()
        {
            systemRandom = new System.Random();
        }

        /// <summary>
        /// Generates a random number in Range. Max is exclusive
        /// </summary>
        /// <param name="min">Inclusive minimum value.</param>
        /// <param name="max">Exclusive maximum value.</param>
        /// <returns>A random number within range</returns>
        public static int Range(int min, int max)
        {
            return systemRandom.Next(min, max);
        }

        /// <summary>
        /// Range the specified min and max, inclusive.
        /// </summary>
        /// <param name="min">Inclusive minimum value.</param>
        /// <param name="max">Inclusive maximum value.</param>
        /// <returns>A random number in range</returns>
        public static float Range(float min, float max)
        {
            return (float)(systemRandom.NextDouble() * (max - min)) + min;
        }

        public static bool TrueFalse()
        {
            return new bool[] { true, false }.ChooseOne();
        }
    }
}