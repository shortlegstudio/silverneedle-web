// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.Magic
{
    using System.Collections.Generic;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;
    public class ArcaneSchool : LevelingClassFeature, IArcaneSchool
    {
        [ObjectStoreOptional("no-opposition-schools")]
        public bool NoOppositionSchools { get; private set; }
        public ArcaneSchool(IObjectStore configuration) : base(configuration)
        {
            configuration.Deserialize(this);
        }
        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }

        public static ArcaneSchool CreateForTesting(string name, bool ignoreOpposition)
        {
            var yaml = @"
name: {0}
no-opposition-schools: {1}".Formatted(name, ignoreOpposition);
            var arcane = new ArcaneSchool(yaml.ParseYaml());
            arcane.NoOppositionSchools = ignoreOpposition;
            return arcane;
        }
    }
}