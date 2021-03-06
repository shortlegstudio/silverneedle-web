// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using System.Linq;
    using System.Collections.Generic;
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    
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

            barbarian = CharacterTestTemplates.Barbarian();
        }

        [Fact]
        public void SelectFromRagePowersThatCharacterIsQualifiedFor()
        {
            ragePowerSelector.Execute(barbarian.Components);
            Assert.Equal(barbarian.Components.Get<RagePower>().Name, "Rage 1");
        }

        [Theory]
        [Repeat(200)]
        public void DoNotSelectTheSameRagePowerTwice()
        {
            barbarian.SetLevel(8);
            ragePowerSelector.Execute(barbarian.Components);
            ragePowerSelector.Execute(barbarian.Components);
            var powers = barbarian.GetAll<RagePower>().Select(p => p.Name);
            AssertExtensions.Contains("Rage 1", powers);
            AssertExtensions.Contains("Rage 2", powers);
        }
        

        private string rageYaml =@"
- name: Rage 1
- name: Rage 2
  prerequisites:
    - classlevel: Barbarian 8
";
    }
}