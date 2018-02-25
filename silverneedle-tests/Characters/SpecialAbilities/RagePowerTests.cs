// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;
    
    
    public class RagePowerTests
    {
        [Fact]
        public void RagePowersMustHaveNames()
        {
            var data = damageReduction.ParseYaml().Children.First();
            var power = new RagePower(data);
            Assert.Equal(power.Name, "Increased Damage Reduction 3");
        }
        [Fact]
        public void RagePowersHavePrerequisites()
        {
            var data = damageReduction.ParseYaml().Children.First();
            var power = new RagePower(data);
            Assert.Equal(power.Prerequisites.Count, 2);
        }

        [Fact]
        public void IfCharacterDoesNotQualifyRagePowerShouldNotify()
        {
            var data = needsLevel.ParseYaml().Children.First();
            var power = new RagePower(data);  
            var character = CharacterTestTemplates.Barbarian();
            Assert.False(power.IsQualified(character.Components));
            character.SetLevel(8);
            Assert.True(power.IsQualified(character.Components));
        }

        [Fact]
        public void DoesNotNeedPrerequisites()
        {
            var data = nopreReq.ParseYaml().Children.First();
            var power = new RagePower(data);
            Assert.Equal(power.Prerequisites.Count, 0);
        }

        private string nopreReq = @"
- name: Some Power
";

        private string damageReduction = @"
- name: Increased Damage Reduction 3
  prerequisites:
    - classlevel: Barbarian 8
    - ability: Increased Damage Reduction 1
";

        private string needsLevel = @"
- name: Super Power
  prerequisites:
    - classlevel: Barbarian 8
";
    }
}