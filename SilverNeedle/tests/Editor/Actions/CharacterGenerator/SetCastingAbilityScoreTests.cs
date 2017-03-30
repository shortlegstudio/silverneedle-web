// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator
{
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator;
    using SilverNeedle.Characters;

    [TestFixture]
    public class SetCastingAbilityScoreTests
    {
        [Test]
        public void IfNotACasterDoNothing()
        {
            var character = new CharacterSheet();
            var subject = new SetCastingAbilityScore();
            subject.Process(character, new CharacterBuildStrategy());
            Assert.That(character.SpellCasting.CastingAbility, Is.Null);
        }
    }
}