// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Prerequisites
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Prerequisites;
    using SilverNeedle.Serialization;
    using Xunit;

    public class WeaponProficiencyPrerequisiteTests : RequiresDataFiles
    {
        [Fact]
        public void ChecksWhetherProficientWithTypeOfWeapon()
        {
            var configure = new MemoryStore();
            configure.SetValue("weapon", "martial");
            var prof = new WeaponProficiencyPrerequisite(configure);

            var bob = CharacterTestTemplates.AverageBob();
            Assert.False(prof.IsQualified(bob.Components));
            bob.Add(new WeaponProficiency("martial"));
            Assert.True(prof.IsQualified(bob.Components));
        }

        [Fact]
        public void ChecksProficiencyWithSpecificWeapon()
        {
            var configure = new MemoryStore();
            configure.SetValue("weapon", "crossbow");
            var prof = new WeaponProficiencyPrerequisite(configure);

            var bob = CharacterTestTemplates.AverageBob();
            bob.Add(new WeaponProficiency("crossbow"));
            Assert.True(prof.IsQualified(bob.Components));
        }

        [Fact]
        public void CharactersProficientInGroupsOfWeaponsPassSpecificProficiencyTests()
        {
            var configure = new MemoryStore();
            configure.SetValue("weapon", "crossbow");
            var prof = new WeaponProficiencyPrerequisite(configure);

            var bob = CharacterTestTemplates.AverageBob();
            bob.Add(new WeaponProficiency("simple"));
            Assert.True(prof.IsQualified(bob.Components));
        }
    }
}