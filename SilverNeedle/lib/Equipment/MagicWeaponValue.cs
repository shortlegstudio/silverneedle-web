// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Equipment
{
    using System;
    using SilverNeedle.Serialization;

    [ObjectStoreSerializable]
    public class MagicWeaponValue : IGatewayObject
    {
        [ObjectStore("modifier")]
        public int Modifier { get; set; }
        [ObjectStore("value")]
        public string Value { get; set; }
        public bool Matches(string name)
        {
            return this.Modifier == int.Parse(name);
        }
    }
}