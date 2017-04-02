// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Equipment
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    public static class ArmorGatewayExtensions
    {
        public static IEnumerable<Armor> FindByProficiency(this EntityGateway<Armor> gateway, 
            IEnumerable<ArmorProficiency> proficiencies)
        {
            return gateway.Where(
                x => proficiencies.IsProficient(x));
        }

         /// <summary>
        /// Finds the type of the by armor.
        /// </summary>
        /// <returns>The armor by type.</returns>
        /// <param name="type">Type of armor.</param>
        public static IEnumerable<Armor> FindByArmorType(this EntityGateway<Armor> gateway, ArmorType type)
        {
            return gateway.Where(x => x.ArmorType == type);
        }

        /// <summary>
        /// Finds the by armor types.
        /// </summary>
        /// <returns>The armors matching types.</returns>
        /// <param name="types">Types of armor.</param>
        public static IEnumerable<Armor> FindByArmorTypes(this EntityGateway<Armor> gateway, params ArmorType[] types)
        {
            return gateway.Where(x => types.Contains(x.ArmorType));
        }

    }
}