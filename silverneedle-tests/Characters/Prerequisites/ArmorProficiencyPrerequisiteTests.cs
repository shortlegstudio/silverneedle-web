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

    public class ArmorProficiencyPrerequisiteTests
    {
        [Fact]
        public void IfCharacterCanUseShieldThanPass()
        {
            var config = new MemoryStore();
            config.SetValue("armor", "shield");
            var prof = new ArmorProficiencyPrerequisite(config);

            var bob = CharacterTestTemplates.AverageBob();
            Assert.False(prof.IsQualified(bob.Components));
            bob.Add(new ArmorProficiency("shield"));
            Assert.True(prof.IsQualified(bob.Components));
        }

        [Fact]
        public void IfFlagNotProficientItPassesIfNotProficientAndFailsIfProficient()
        {
            var config = new MemoryStore();
            config.SetValue("armor", "shield");
            config.SetValue("not-proficient", true);
            var prof = new ArmorProficiencyPrerequisite(config);

            var bob = CharacterTestTemplates.AverageBob();
            Assert.True(prof.IsQualified(bob.Components));
            bob.Add(new ArmorProficiency("shield"));
            Assert.False(prof.IsQualified(bob.Components));
        }
    }
}