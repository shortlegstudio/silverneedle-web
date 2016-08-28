// //-----------------------------------------------------------------------
// // <copyright file="BasicStatModifiedEventArgs.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
namespace SilverNeedle
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using SilverNeedle.Characters;

    /// <summary>
    /// Basic stat modified event arguments.
    /// </summary>
    public class BasicStatModifiedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.BasicStatModifiedEventArgs"/> class.
        /// </summary>
        /// <param name="oldBase">Old base value for statistic.</param>
        /// <param name="newBase">New base value for statistic.</param>
        /// <param name="oldTotal">Old total value for statistic.</param>
        /// <param name="newTotal">New total value for statistic.</param>
        public BasicStatModifiedEventArgs(
            int oldBase,
            int newBase,
            int oldTotal,
            int newTotal)
        {
            this.OldBaseValue = oldBase;
            this.NewBaseValue = newBase;
            this.OldTotalValue = oldTotal;
            this.NewTotalValue = newTotal;
        }

        /// <summary>
        /// Gets or sets the old base value for the statistic that changed
        /// </summary>
        /// <value>The old base value.</value>
        public int OldBaseValue { get; set; }

        /// <summary>
        /// Gets or sets the new base value for the statistic that changed
        /// </summary>
        /// <value>The new base value.</value>
        public int NewBaseValue { get; set; }

        /// <summary>
        /// Gets or sets the old total value for the statistic that changed
        /// </summary>
        /// <value>The old total value.</value>
        public int OldTotalValue { get; set; }

        /// <summary>
        /// Gets or sets the new total value for the statistic that changed
        /// </summary>
        /// <value>The new total value.</value>
        public int NewTotalValue { get; set; }
    }
}
