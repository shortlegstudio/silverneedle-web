// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Equipment
{
    /// <summary>
    /// Piece of equipment
    /// </summary>
    public interface IGear : INamedEntity
    {
        /// <summary>
        /// Gets the weight.
        /// </summary>
        /// <value>The weight of the equipment.</value>
        float Weight { get; }

        int Value { get; }

        bool GroupSimilar { get; }
    }
}