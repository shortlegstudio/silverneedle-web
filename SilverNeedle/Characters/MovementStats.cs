//-----------------------------------------------------------------------
// <copyright file="MovementStats.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Characters
{
    using System;

    /// <summary>
    /// Handles movement stats like how to work with armor or flying etc...
    /// </summary>
    public class MovementStats : IStatTracker
    {
        /// <summary>
        /// The size of a square
        /// </summary>
        private const int SquareSize = 5;

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.MovementStats"/> class.
        /// Defaults to 30' speed
        /// </summary>
        public MovementStats()
            : this(30)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Characters.MovementStats"/> class.
        /// </summary>
        /// <param name="baseSpeed">Base speed for the character.</param>
        public MovementStats(int baseSpeed)
        {
            this.BaseMovement = new BasicStat(baseSpeed);
        }

        /// <summary>
        /// Gets the base movement.
        /// </summary>
        /// <value>The base movement.</value>
        public BasicStat BaseMovement { get; private set; }

        /// <summary>
        /// Gets the base movement in squares
        /// </summary>
        /// <value>The base movement in squares.</value>
        public int BaseSquares
        { 
            get { return this.BaseMovement.TotalValue / SquareSize; }
        }

        /// <summary>
        /// The implementing class must handle modifiers to stats under its control
        /// </summary>
        /// <param name="modifier">Modifier for stats</param>
        public void ProcessModifier(IModifiesStats modifier)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the base speed.
        /// </summary>
        /// <param name="speed">Base speed of the character.</param>
        public void SetBaseSpeed(int speed)
        {
            this.BaseMovement.SetValue(speed);
        }
    }
}