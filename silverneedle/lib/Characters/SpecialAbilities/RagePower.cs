// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using System;
    using SilverNeedle.Characters.Prerequisites;
    using SilverNeedle.Serialization;
    public class RagePower : Feature, IHasPrerequisites, IGatewayObject
    {
        public RagePower(IObjectStore configuration) : base(configuration)
        {
            var prereq = configuration.GetObjectListOptional("prerequisites");
            Prerequisites = new PrerequisiteList(prereq);
        }

        public PrerequisiteList Prerequisites { get; private set; }

        public bool IsQualified(Utility.ComponentContainer components)
        {
            return Prerequisites.IsQualified(components);
        }

        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }

        public string DisplayString()
        {
            return this.Name;
        }

    }
}