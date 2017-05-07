// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;
    
    [TestFixture]
    public class RagePowerTests
    {
        [Test]
        public void RagePowersHavePrerequisites()
        {
            var data = damageReduction.ParseYaml().Children.First();
            var power = new RagePower(data);
            Assert.That(power.Prerequisites.Count, Is.EqualTo(2));
        }

        [Test]
        public void IfCharacterDoesNotQualifyRagePowerShouldNotify()
        {
            var data = needsLevel.ParseYaml().Children.First();
            var power = new RagePower(data);  
            var character = new CharacterSheet();
            Assert.That(power.IsQualified(character), Is.False);
            var barbarian = new Class();
            barbarian.Name = "Barbarian";
            character.SetClass(barbarian);
            character.SetLevel(8);
            Assert.That(power.IsQualified(character), Is.True);
        }


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