// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.ClassFeatures
{
    using System.Collections.Generic;
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    [TestFixture]
    public class SelectRagePowerTests
    {
        SelectRagePower ragePowerSelector;
        CharacterSheet barbarian;

        [SetUp]
        public void Configure()
        {
            var parsed = rageYaml.ParseYaml();
            var powers = new List<RagePower>();
            foreach(var r in parsed.Children)
            {
                powers.Add(new RagePower(r));
            }
            var gateway = new EntityGateway<RagePower>(powers);
            ragePowerSelector = new SelectRagePower(gateway);

            barbarian = new CharacterSheet();
            var cls = new Class();
            cls.Name = "Barbarian";
            barbarian.SetClass(cls);
        }

        [Test]
        public void SelectFromRagePowersThatCharacterIsQualifiedFor()
        {
            ragePowerSelector.Process(barbarian, new CharacterBuildStrategy());
            Assert.That(barbarian.Components.Get<RagePower>().Name, Is.EqualTo("Rage 1"));
        }

        private string rageYaml =@"
- name: Rage 1
- name: Rage 2
  prerequisites:
    - classlevel: Barbarian 8
";
    }
}