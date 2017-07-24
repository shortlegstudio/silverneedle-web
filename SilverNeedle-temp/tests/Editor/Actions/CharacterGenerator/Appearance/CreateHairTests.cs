// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.Appearance
{
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.Appearance;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Appearance;
    using SilverNeedle.Serialization;

    [TestFixture]
    public class CreateHairTests
    {
        [Test]
        public void ProcessCreatesADescriptionCombiningColorAndStyle()
        {
            var colors = new HairColor[] { new HairColor("copper") };
            var styles = new HairStyle[] { new HairStyle("ponytail") };
            styles[0].Descriptors.Add("descriptor", new string[] { "long" });

            var subject = new CreateHair(new EntityGateway<HairColor>(colors), new EntityGateway<HairStyle>(styles));
            var character = new CharacterSheet();
            subject.Process(character, new CharacterBuildStrategy());
            Assert.That(character.Appearance.Hair, Is.EqualTo("copper long ponytail"));
        }
    }
}