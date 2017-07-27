// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.CustomClassSteps
{
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.CustomClassSteps;
    using SilverNeedle.Characters;

    [TestFixture]
    public class CommonerCustomStepsTests
    {
        [Fact]
        public void SelectsASingleSimpleWeaponProficiency()
        {
            var character = new CharacterSheet();

            var subject = new CommonerCustomSteps();
            subject.Process(character, new CharacterBuildStrategy());
            Assert.That(character.Offense.WeaponProficiencies.Count, Is.EqualTo(1));
        }
    }
}