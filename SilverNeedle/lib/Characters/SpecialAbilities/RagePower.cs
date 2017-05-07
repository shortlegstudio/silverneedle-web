// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using System;
    using SilverNeedle.Characters.Prerequisites;
    using SilverNeedle.Serialization;
    public class RagePower : IHasPrerequisites
    {
        public RagePower(IObjectStore configuration)
        {
            var prereq = configuration.GetObjectOptional("prerequisites");
            Prerequisites = new PrerequisiteList(prereq);
        }

        public PrerequisiteList Prerequisites { get; private set; }

        public bool IsQualified(CharacterSheet character)
        {
            return Prerequisites.IsQualified(character);
        }
    }
}