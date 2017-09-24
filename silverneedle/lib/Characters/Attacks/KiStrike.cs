// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Attacks
{
    using SilverNeedle.Serialization;
    public class KiStrike 
    {
        public KiStrike(string damageType)
        {
            this.DamageType = damageType;
        }

        public KiStrike(IObjectStore configuration)
        {
            this.DamageType = configuration.GetString("damage-type");
        }
        public string DamageType { get; private set; }
    }
}