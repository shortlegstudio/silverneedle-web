// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle
{
    public interface IStatModifier
    {
        float Modifier { get; }
        string Reason { get; }
        string ModifierType { get; }
        string StatisticName { get; }
        string Condition { get; }
        string StatisticType { get; }
    }
}