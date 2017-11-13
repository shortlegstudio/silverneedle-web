// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.ClassFeatures
{
    using System.Collections.Generic;
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Characters.Attacks;

    public class AddBloodlinePowerTests
    {
        [Fact]
        public void AddsAPowerThatIsAvailableAtCurrentLevel()
        {
            var bloodline = Bloodline.CreateWithValues(
                "draconic",
                "perception",
                new Dictionary<int, string>() { { 3, "SilverNeedle.Characters.Attacks.AcidicRay" }},
                new Dictionary<int, string>(),
                new string[] { }
            );

            var character = CharacterTestTemplates.Sorcerer().WithSkills();
            character.Add(bloodline);
            var addPower = new AddBloodlinePower();
            character.SetLevel(3);
            addPower.ExecuteStep(character);
            Assert.NotNull(character.Get<AcidicRay>());
        }
    }
}