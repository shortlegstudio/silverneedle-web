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
    using SilverNeedle.Utility;

    [TestFixture]
    public class RogueTalentTests
    {
        [Test]
        public void ParseAdvancedTalents()
        {
            var talentYaml = @"
- name: Talent
  advanced-talent: true".ParseYaml().Children.First();
            var talent = new RogueTalent(talentYaml);
            Assert.That(talent.Name, Is.EqualTo("Talent"));
            Assert.That(talent.IsAdvancedTalent, Is.True);
        }

        [Test]
        public void ParseSneakAttackTalents()
        {
            var talentYaml = @"
- name: Talent
  sneak-attack: true".ParseYaml().Children.First();
            var talent = new RogueTalent(talentYaml);
            Assert.That(talent.Name, Is.EqualTo("Talent"));
            Assert.That(talent.IsSneakAttack, Is.True);
        }
    }
}