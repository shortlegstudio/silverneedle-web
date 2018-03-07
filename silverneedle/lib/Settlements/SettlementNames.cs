// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Settlements
{
    using SilverNeedle.Serialization;

    [ObjectStoreSerializable]
    public class SettlementNames : IGatewayObject
    {
        public bool Matches(string name)
        {
            return this.Group.EqualsIgnoreCase(name);
        }

        [ObjectStore("group")]
        public string Group { get; private set; }

        [ObjectStore("names")]
        public string[] Names { get; private set; }
    }
}