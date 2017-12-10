// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    public class PowerOverUndeadTests
    {
        [Fact]
        public void GoodCreaturesTakeTurnUndead()
        {
            var wizard = CharacterTestTemplates.Wizard();
            wizard.Alignment = CharacterAlignment.ChaoticGood;
            var power = new PowerOverUndead();
            wizard.Add(power);
            AssertCharacter.HasFeatToken("Turn Undead", wizard);
        }

        [Fact]
        public void EvilCreaturesTakeCommandUndead()
        {
            var wizard = CharacterTestTemplates.Wizard();
            wizard.Alignment = CharacterAlignment.ChaoticEvil;
            var power = new PowerOverUndead();
            wizard.Add(power);
            AssertCharacter.HasFeatToken("Command Undead", wizard);
        }
    }
}