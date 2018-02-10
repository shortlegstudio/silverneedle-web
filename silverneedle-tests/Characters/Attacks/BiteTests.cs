// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Attacks
{
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Serialization;

    public class BiteTests
    {
        [Fact]
        public void BasicYamlConfiguration()
        {
            var yaml = @"
name: Bite
attack-type: Melee
damage: 
  name: Bite Damage
  dice: 1d4
attack-bonus:
  name: Bite Attack Bonus
  base-value: 0";

            var bite = new Bite(yaml.ParseYaml());
            Assert.Equal("Bite", bite.Name);
            Assert.Equal(AttackTypes.Melee, bite.AttackType);
            Assert.Equal("1d4", bite.Damage.ToString());
            Assert.Equal("Bite +0 (1d4)", bite.DisplayString());

            var bob = CharacterTestTemplates.AverageBob();
            bob.Add(bite);
            Assert.NotNull(bob.FindStat("Bite Attack Bonus"));
            Assert.NotNull(bob.FindStat("Bite Damage"));
        }

        [Fact]
        public void ConvertsDamageBasedOnCharacterSize()
        {
            var bob = CharacterTestTemplates.AverageBob();
            bob.Size.SetSize(CharacterSize.Small, 4, 120);

            var yaml = @"
name: Bite
attack-type: Melee
damage: 
  name: Bite Damage
  dice: 1d4
attack-bonus:
  name: Bite Attack Bonus
  base-value: 0";
            var bite = new Bite(yaml.ParseYaml());
            bob.Add(bite);

            var modYaml = @"
name: Bite Damage";
            var sizeMod = new ConvertDamageDiceOnSizeModifier(modYaml.ParseYaml());
            bob.Add(sizeMod);

            Assert.Equal("1d3", bite.Damage.ToString());

        }
    }
}