// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.Personality
{
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.Personality;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Personalities;

    [TestFixture]
    public class SelectQuirksTests
    {
        [Test]
        public void SelectsSomeQuirksFromAvailableList()
        {
            var character = new CharacterSheet();
            var step = new SelectQuirks();
            step.Process(character, new CharacterBuildStrategy());
            var quirks = character.Get<Quirks>();
            Assert.That(quirks, Is.Not.Null);
            Assert.That(quirks.Items, Is.Not.Empty);
        }
    }
}