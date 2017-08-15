// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.ClassFeatures
{
    using System.Collections.Generic;
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    
    public class SelectRagePowerTests
    {
        SelectRagePower ragePowerSelector;
        CharacterSheet barbarian;

        public SelectRagePowerTests()
        {
            var parsed = rageYaml.ParseYaml();
            var powers = new List<RagePower>();
            foreach(var r in parsed.Children)
            {
                powers.Add(new RagePower(r));
            }
            var gateway = EntityGateway<RagePower>.LoadFromList(powers);
            ragePowerSelector = new SelectRagePower(gateway);

            barbarian = new CharacterSheet();
            var cls = new Class();
            cls.Name = "Barbarian";
            barbarian.SetClass(cls);
        }

        [Fact]
        public void SelectFromRagePowersThatCharacterIsQualifiedFor()
        {
            ragePowerSelector.ExecuteStep(barbarian, new CharacterBuildStrategy());
            Assert.Equal(barbarian.Components.Get<RagePower>().Name, "Rage 1");
        }

        private string rageYaml =@"
- name: Rage 1
- name: Rage 2
  prerequisites:
    - classlevel: Barbarian 8
";
    }
}