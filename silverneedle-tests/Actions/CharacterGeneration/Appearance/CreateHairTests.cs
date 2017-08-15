// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.Appearance
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.Appearance;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Appearance;
    using SilverNeedle.Serialization;

    
    public class CreateHairTests
    {
        [Fact]
        public void ProcessCreatesADescriptionCombiningColorAndStyle()
        {
            var colors = new HairColor[] { new HairColor("copper") };
            var styles = new HairStyle[] { new HairStyle("ponytail") };
            styles[0].Descriptors.Add("descriptor", new string[] { "long" });

            var subject = new CreateHair(EntityGateway<HairColor>.LoadFromList(colors), EntityGateway<HairStyle>.LoadFromList(styles));
            var character = new CharacterSheet();
            subject.ExecuteStep(character, new CharacterBuildStrategy());
            Assert.Equal(character.Appearance.Hair, "copper long ponytail");
        }
    }
}