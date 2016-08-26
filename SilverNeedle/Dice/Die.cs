//-----------------------------------------------------------------------
// <copyright file="Die.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Dice
{
    using SilverNeedle;

    /// <summary>
    /// A die represents a random whole number range
    /// </summary>
    public class Die
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SilverNeedle.Dice.Die"/> class.
        /// </summary>
        /// <param name="sides">Sides of the die</param>
        public Die(DiceSides sides)
        {
            this.Sides = sides;
            this.LastRoll = -1;
        }

        /// <summary>
        /// Gets the sides of the die
        /// </summary>
        /// <value>The sides of the die</value>
        public DiceSides Sides { get; private set; }

        /// <summary>
        /// Gets the last roll of this die
        /// </summary>
        /// <value>The last roll.</value>
        public int LastRoll { get; private set; }

        /// <summary>
        /// Gets the numeric number of sides of this die
        /// </summary>
        /// <value>The side count.</value>
        public int SideCount
        { 
            get 
            { 
                return (int)this.Sides; 
            }
        }

        /// <summary>
        /// Gets the maximmum value that could be rolled on a die. 
        /// </summary>
        private int MaximumValue 
        { 
            get 
            { 
                return this.SideCount + 1; 
            } 
        }

        /// <summary>
        /// Generates an array of die objects based on the the numbers of sides and count specified
        /// </summary>
        /// <returns>The dice array</returns>
        /// <param name="sides">Sides of all the dice</param>
        /// <param name="count">Count of dice to create</param>
        public static Die[] GetDice(DiceSides sides, int count)
        {
            var result = new Die[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = new Die(sides);
            }

            return result;
        }

        /// <summary>
        /// Returns a single four sided die
        /// </summary>
        /// <returns>d4 die</returns>
        public static Die D4()
        {
            return new Die(DiceSides.d4);
        }

        /// <summary>
        /// Returns a single six sided die
        /// </summary>
        /// <returns>d6 die</returns>
        public static Die D6()
        {
            return new Die(DiceSides.d6);
        }

        /// <summary>
        /// Returns a single eight sided die
        /// </summary>
        /// <returns>d8 die</returns>
        public static Die D8()
        {
            return new Die(DiceSides.d8);
        }

        /// <summary>
        /// Returns a single ten sided die
        /// </summary>
        /// <returns>d10 die</returns>
        public static Die D10()
        {
            return new Die(DiceSides.d10);
        }

        /// <summary>
        /// Returns a single twelve sided die
        /// </summary>
        /// <returns>d12 die</returns>
        public static Die D12()
        {
            return new Die(DiceSides.d12);
        }

        /// <summary>
        /// Returns a single twenty sided die
        /// </summary>
        /// <returns>d20 die</returns>
        public static Die D20()
        {
            return new Die(DiceSides.d20);
        }

        /// <summary>
        /// Returns a single hundred sided die
        /// </summary>
        /// <returns>d100 die</returns>
        public static Die D100()
        {
            return new Die(DiceSides.d100);
        }

        /// <summary>
        /// Rolls the die. This generates a random number from 1 to SidesCount.
        /// A d4 would generate either 1, 2, 3, 4
        /// </summary>
        /// <returns>The value of the roll</returns>
        public int Roll()
        {
            this.LastRoll = Randomly.Range(1, this.MaximumValue); 
            return this.LastRoll;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="SilverNeedle.Dice.Die"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current <see cref="SilverNeedle.Dice.Die"/>.</returns>
        public override string ToString()
        {
            return string.Format("[Die: Sides={0}, LastRoll={1}]", this.Sides, this.LastRoll);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="SilverNeedle.Dice.Die"/>.
        /// This requires the dice to be the same type and have the same roll
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="SilverNeedle.Dice.Die"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
        /// <see cref="SilverNeedle.Dice.Die"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            var die = obj as Die;
            if (die == null)
            {
                return false;
            }

            if (die.Sides != this.Sides)
            {
                return false;
            }

            return die.LastRoll == this.LastRoll;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="SilverNeedle.Dice.Die"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
