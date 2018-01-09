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
            configuration.Deserialize(this);
        }

        [ObjectStoreOptional("at-will")]
        public bool AtWill { get; private set; }

        [ObjectStoreOptional("per-day")]
        public int UsesPerDay { get; private set; }

        [ObjectStore("spell")]
        public string Spell { get; private set; }
        public string DisplayString()
        {
            if(AtWill)
                return "At will - {0}".Formatted(Spell);

            return "{0}/day - {1}".Formatted(UsesPerDay, Spell);
        }

    }

}