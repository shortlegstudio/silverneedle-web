// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using System;
    using SilverNeedle.Characters.Prerequisites;
    using SilverNeedle.Serialization;
    public class RogueTalent : SpecialAbility, IGatewayObject, IHasPrerequisites
    {
        public bool IsAdvancedTalent { get; set; }
        public bool IsSneakAttack { get; set; }

        public PrerequisiteList Prerequisites { get; private set; }

        public RogueTalent(IObjectStore data)
        {
            this.Name = data.GetString("name");
            this.IsAdvancedTalent = data.GetBoolOptional("advanced-talent");
            this.IsSneakAttack = data.GetBoolOptional("sneak-attack");
            this.Prerequisites = new PrerequisiteList(data.GetObjectOptional("prerequisites"));
        }
        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }

        public bool IsQualified(Utility.ComponentContainer components)
        {
            return this.Prerequisites.IsQualified(components);
        }
    }
}