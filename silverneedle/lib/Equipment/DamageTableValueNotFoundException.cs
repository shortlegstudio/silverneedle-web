// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Equipment
{
    public class DamageTableValueNotFoundException : System.Exception
    {
        public DamageTableValueNotFoundException() { }
        public DamageTableValueNotFoundException(string damageType) : base(string.Format("Missing Damage Type: {0}", damageType)) { }
    }
}