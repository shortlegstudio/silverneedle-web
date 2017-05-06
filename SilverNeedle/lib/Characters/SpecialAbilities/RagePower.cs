// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Characters.Prerequisites;
    using SilverNeedle.Serialization;
    public class RagePower 
    {
        public RagePower(IObjectStore configuration)
        {
            var prereq = configuration.GetObjectOptional("prerequisites");
            Prerequisites = new PrerequisiteList(prereq);
        }

        public PrerequisiteList Prerequisites { get; private set; }

    }
}