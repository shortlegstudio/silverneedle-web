// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Prerequisites
{
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Prerequisites;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    
    public class PrerequisiteTests {
        [Fact]
        public void ParseSomeYaml() {
            var yamlNode = PrerequisitesYaml.ParseYaml();
            var prereq = yamlNode.GetObject("prerequisites");

            var prereqs = new PrerequisiteList(prereq);

            Assert.Equal (5, prereqs.Count);
            Assert.IsType<AbilityPrerequisite>(prereqs.First());

        }

        [Fact]
        public void AlwaysQualifiedIfNoQualificationsNeeded() {
            var pre = new PrerequisiteList();
            Assert.True(pre.IsQualified(new CharacterSheet(CharacterStrategy.Default())));
        }

        [Fact]
        public void EmptyNodesJustMakeNoPrerequisites()
        {
            var data = new MemoryStore();
            var list = new PrerequisiteList(data);
            Assert.Equal(list.Count, 0);
        }

        [Fact]
        public void NullObjectDataHappensWhenNoPrerequisitesJustIgnore()
        {
            var list = new PrerequisiteList(null);
            Assert.Equal(list.Count, 0);
        }
        private const string PrerequisitesYaml = @"--- 
prerequisites:
  - intelligence: 13
  - race: Elf
  - ability: Weapon Finesse
  - skillranks: Acrobatics 4
  - ability: darkvision
";
    }
}