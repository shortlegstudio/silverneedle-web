// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.ClassFeatures
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.ClassFeatures;
    using SilverNeedle.Characters.SpecialAbilities;
    using System.Collections.Generic;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    public class AddBloodlineBonusFeatTests : RequiresDataFiles
    {
        [Fact]
        public void AddsAFeatTokenFromTheListOfOptions()
        {
            var adder = new AddBloodlineBonusFeat();
            var sorcerer = CharacterTestTemplates.Sorcerer();
            var bloodline = Bloodline.CreateWithValues(
                "arcane",
                new string[] { "perception" },
                new Dictionary<int, string>(),
                new Dictionary<int, string>(),
                new string[] { "Improved Initiative" }
            );
            sorcerer.Add(bloodline);

            adder.ExecuteStep(sorcerer);
            var featToken = sorcerer.Get<FeatToken>();
            Assert.NotNull(featToken);
            Assert.Contains("Improved Initiative", featToken.Tags);
        }
    }
}