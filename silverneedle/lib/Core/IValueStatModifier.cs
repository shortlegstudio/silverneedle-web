// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle
{
    public interface IValueStatModifier : IStatisticModifier
    {
        float Modifier { get; }
        string ModifierType { get; }
        string Condition { get; }
    }
}