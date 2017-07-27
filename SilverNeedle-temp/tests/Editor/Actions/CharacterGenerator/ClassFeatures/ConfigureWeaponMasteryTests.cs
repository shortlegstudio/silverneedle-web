// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.ClassFeatures
{
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    
    [TestFixture]
    public class ConfigureWeaponMasteryTests
    {
        [Fact]
        public void ChoosesAWeaponForMastery()
        {
            var configure = new ConfigureWeaponMastery();
            var character = new CharacterSheet();
            configure.Process(character, new CharacterBuildStrategy());

            var mastery = character.Components.Get<WeaponMastery>();
            Assert.That(mastery.Weapon, Is.Not.Null);
        }
    }
}