// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.SpellCasting
{
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.SpellCasting;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;

    [TestFixture]
    public class SetCastingAbilityScoreTests
    {
        [Test]
        public void IfNotACasterDoNothing()
        {
            var character = new CharacterSheet();
            character.SetClass(Class.None);
            var subject = new SetCastingAbilityScore();
            subject.Process(character, new CharacterBuildStrategy());
            Assert.That(character.Get<SpellCasting>().CastingAbility, Is.Null);
        }
    }
}