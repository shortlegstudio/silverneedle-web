// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle
{
    public interface IDiceStatistic : IStatistic
    {
        string DisplayString();
        Dice.Cup Dice { get; }
    }
}