// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Equipment
{
    using System.Collections.Generic;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    public static class WeaponGatewayExtensions
    {
        public static IEnumerable<Weapon> FindByProficient(this EntityGateway<Weapon> gateway, 
            IEnumerable<WeaponProficiency> proficiencies)
        {
            return gateway.Where(x => proficiencies.IsProficient(x));
        }

        public static IEnumerable<Weapon> SimpleWeapons(this EntityGateway<Weapon> gateway)
        {
            return gateway.Where(x => x.Level == WeaponTrainingLevel.Simple);
        }
    }
}