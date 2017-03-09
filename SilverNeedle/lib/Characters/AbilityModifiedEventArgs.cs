// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System;
    
    /// <summary>
    /// Ability modified event arguments are passed whenever an ability changes.
    /// </summary>
    public class AbilityModifiedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the ability that was modified
        /// </summary>
        public AbilityScore Ability { get; set; }
    }   
}