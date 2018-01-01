// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Utility
{
    public interface IWeightedOptionTable
    {
        IWeightedOptionTable Copy(bool includeDisabled = true);
    }
}