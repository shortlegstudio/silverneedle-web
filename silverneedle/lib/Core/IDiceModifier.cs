// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle
{
    using SilverNeedle.Dice;
    public interface IDiceModifier : IStatisticModifier
    {
        void ProcessModifier(Cup dice);
    }
}