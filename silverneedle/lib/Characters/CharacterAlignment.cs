//-----------------------------------------------------------------------
// <copyright file="CharacterAlignment.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle.Characters
{
    /// <summary>
    /// Represents character alignment or their guiding priniciples
    /// </summary>
    public enum CharacterAlignment
    {
        /// <summary>
        /// A lawful good character typically acts with compassion and always with honor and a sense of duty.
        /// </summary>
        LawfulGood,

        /// <summary>
        /// A neutral good character typically acts altruistically, without regard for or against lawful precepts such as rules or tradition.
        /// </summary>
        NeutralGood,

        /// <summary>
        /// A chaotic good character does what is necessary to bring about change for the better, 
        /// disdains bureaucratic organizations that get in the way of social improvement, 
        /// and places a high value on personal freedom, not only for oneself, but for others as well.
        /// </summary>
        ChaoticGood,

        /// <summary>
        /// A lawful neutral character typically believes strongly in lawful concepts such as honor, order, rules, and tradition, and often follows a personal code.
        /// </summary>
        LawfulNeutral,

        /// <summary>
        /// A neutral character (a.k.a. true neutral) is neutral on both axes and tends not to feel strongly towards any alignment, or actively seeks their balance.
        /// </summary>
        Neutral,

        /// <summary>
        /// A chaotic neutral character is an individualist who follows their own heart and generally shirks rules and traditions.
        /// </summary>
        ChaoticNeutral,

        /// <summary>
        /// A lawful evil character sees a well-ordered system as being easier to exploit and shows a combination of desirable and undesirable traits.
        /// </summary>
        LawfulEvil,

        /// <summary>
        /// A neutral evil character is typically selfish and has no qualms about turning on its allies-of-the-moment, 
        /// and usually makes allies primarily to further their own goals.
        /// </summary>
        NeutralEvil,

        /// <summary>
        /// A chaotic evil character tends to have no respect for rules, other people's lives, 
        /// or anything but their own desires, which are typically selfish and cruel.
        /// </summary>
        ChaoticEvil
    }
}