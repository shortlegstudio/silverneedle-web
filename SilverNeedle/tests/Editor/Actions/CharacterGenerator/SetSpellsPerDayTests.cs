// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator
{
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator;
    using SilverNeedle.Characters;
    using SilverNeedle.Spells;

    [TestFixture]
    public class SetSpellsPerDayTests
    {
        SetSpellsPerDay subject;
        
        [SetUp]
        public void SetUp()
        {
            subject = new SetSpellsPerDay();
        }

        [Test]
        public void IfNotACasterDoNothing()
        {
            var character = new CharacterSheet();
            subject.Process(character, new CharacterBuildStrategy());
            Assert.That(character.SpellCasting.GetSpellsPerDay(0), Is.EqualTo(0));
        }

        [Test]
        public void SetSpellsAvailableForThatDayForFirstLevel()
        {
            var character = new CharacterSheet();
            character.Class.Spells.PerDay[1] = new int[] { 3, 1 };
            character.SpellCasting.CasterLevel = 1;

            subject.Process(character, new CharacterBuildStrategy());

            Assert.That(character.SpellCasting.GetSpellsPerDay(0), Is.EqualTo(3));
            Assert.That(character.SpellCasting.GetSpellsPerDay(1), Is.EqualTo(1));
        }
    }
}