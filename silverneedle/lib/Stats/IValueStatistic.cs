// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle
{
    using System.Collections.Generic;
    using SilverNeedle.Serialization;

    public interface IValueStatistic : IGatewayObject, IStatistic
    {
        int TotalValue { get; }

        int GetConditionalValue(string condition);
    }
}