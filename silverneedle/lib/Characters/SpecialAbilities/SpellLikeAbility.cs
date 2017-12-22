// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Serialization;

    public class SpellLikeAbility : IAbility
    {
        public SpellLikeAbility(IObjectStore configuration)
        {
            UsesPerDay = configuration.GetInteger("per-day");
            Spell = configuration.GetString("spell");
        }
        public int UsesPerDay { get; private set; }
        public string Spell { get; private set; }
        public string DisplayString()
        {
            return "{0}/day - {1}".Formatted(UsesPerDay, Spell);
        }

    }

}