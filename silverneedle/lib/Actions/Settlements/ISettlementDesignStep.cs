// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.Settlements
{
    using SilverNeedle.Settlements;
    public interface ISettlementDesignStep
    {
        void Execute(Settlement settlement);
    }
}