//-----------------------------------------------------------------------
// <copyright file="IEntityGateway.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SilverNeedle
{
    using System.Collections.Generic;

    /// <summary>
    /// Entity Gateway is an interface defining standard gateway behaviors
    /// </summary>
    /// <typeparam name="T">Type of Entities stored in this gateway</typeparam>
    public interface IEntityGateway<T>
    {
        /// <summary>
        /// Should return all of this entity types
        /// </summary>
        /// <returns>Enumerable collection of the entities</returns>
        IEnumerable<T> All();
    }
}