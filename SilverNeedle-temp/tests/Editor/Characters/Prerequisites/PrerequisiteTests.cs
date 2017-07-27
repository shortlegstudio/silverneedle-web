// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Prerequisites
{
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Prerequisites;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    [TestFixture]
    public class PrerequisiteTests {
        [Fact]
        public void ParseSomeYaml() {
            var yamlNode = PrerequisitesYaml.ParseYaml();
            var prereq = yamlNode.GetObject("prerequisites");

            var prereqs = new PrerequisiteList(prereq);

            Assert.AreEqual (5, prereqs.Count);
            Assert.IsInstanceOf<AbilityPrerequisite>(prereqs.First());

        }

        [Fact]
        public void AlwaysQualifiedIfNoQualificationsNeeded() {
            var pre = new PrerequisiteList();
            Assert.IsTrue(pre.IsQualified(new CharacterSheet(new List<Skill>())));
        }

        [Fact]
        public void EmptyNodesJustMakeNoPrerequisites()
        {
            var data = new MemoryStore();
            var list = new PrerequisiteList(data);
            Assert.That(list.Count, Is.EqualTo(0));
        }

        [Fact]
        public void NullObjectDataHappensWhenNoPrerequisitesJustIgnore()
        {
            var list = new PrerequisiteList(null);
            Assert.That(list.Count, Is.EqualTo(0));
        }
        private const string PrerequisitesYaml = @"--- 
prerequisites:
  - intelligence: 13
  - race: Elf
  - feat: Weapon Finesse
  - skillranks: Acrobatics 4
  - ability: darkvision
";
    }
}