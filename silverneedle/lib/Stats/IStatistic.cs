// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle
{
    using System.Collections.Generic;

    public interface IStatistic
    {
        string Name { get; }
        void AddModifier(IStatisticModifier modifier);
        void RemoveModifier(IStatisticModifier modifier);
        IEnumerable<IStatisticModifier> Modifiers { get; }
        bool Matches(string name);
    }
}