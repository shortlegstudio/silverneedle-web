// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;

    public class WeaponProficiencyExoticToMartialTests
    {
        [Fact]
        public void WeaponsAreTreatedAsMartialForDeterminingProficiencyLevel()
        {
            var config = new MemoryStore();
            config.SetValue("weapons", new string[] { "\"dwarven\"" });
            var exotic = new WeaponProficiencyExoticToMartial(config);
            var martial = new WeaponProficiency("martial");
            var bob = CharacterTestTemplates.AverageBob();
            bob.Add(martial);
            bob.Add(exotic);

            var dwarvenHammer = new Weapon();
            dwarvenHammer.Level = WeaponTrainingLevel.Exotic;
            dwarvenHammer.Name = "Dwarven Hammer";
            Assert.True(bob.Offense.IsProficient(dwarvenHammer));

            var elvenBow = new Weapon();
            elvenBow.Level = WeaponTrainingLevel.Exotic;
            elvenBow.Name = "Elven Bow";
            Assert.False(bob.Offense.IsProficient(elvenBow));

        }
    }
}