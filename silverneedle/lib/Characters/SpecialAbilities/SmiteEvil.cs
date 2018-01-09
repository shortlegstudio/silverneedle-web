// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;
    using SilverNeedle.Serialization;

    public class SmiteEvil : IAbility, IComponent
    {
        public BasicStat UsesPerDay { get; private set; }

        public SmiteEvil()
        {
            UsesPerDay = new BasicStat("Smite Evil Uses Per Day", 0);
        }

        public void Initialize(ComponentContainer components)
        {
            components.Add(UsesPerDay);
        }

        public string DisplayString()
        {
            return string.Format("Smite Evil {0}/day", UsesPerDay.TotalValue);
        }
    }
}