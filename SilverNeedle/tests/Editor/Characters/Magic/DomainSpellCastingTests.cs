// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Magic
{
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;

    [TestFixture]
    public class DomainSpellCastingTests
    {
        [Test]
        public void ShouldReturnOneForSpellsPerDayIfAvailableAtThisLevel()
        {
            var classLevel = new ClassLevel(new Class());
            classLevel.Level = 5;
            var sc = new DomainSpellCasting(new Inventory(), classLevel);
            Assert.That(sc.GetSpellsPerDay(1), Is.EqualTo(1));
            Assert.That(sc.GetSpellsPerDay(2), Is.EqualTo(1));
            Assert.That(sc.GetSpellsPerDay(3), Is.EqualTo(1));
            Assert.That(sc.GetSpellsPerDay(4), Is.EqualTo(0));
            
        }

        [Test]
        public void ShouldReturnZeroForSpellsPerDayOfOrisons()
        {
            var classLevel = new ClassLevel(new Class());
            classLevel.Level = 5;
            var sc = new DomainSpellCasting(new Inventory(), classLevel);
            Assert.That(sc.GetSpellsPerDay(0), Is.EqualTo(0));
        }

        [Test]
        public void MaxLevelIsSpecifiedByClassLevel()
        {
            var classLevel = new ClassLevel(new Class());
            classLevel.Level = 5;
            var sc = new DomainSpellCasting(new Inventory(), classLevel);
            Assert.That(sc.MaxLevel, Is.EqualTo(3));
        }

        [Test]
        public void IfCharacterLevelIs19Or20Just()
        {
            var classLevel = new ClassLevel(new Class());
            classLevel.Level = 20;
            var sc = new DomainSpellCasting(new Inventory(), classLevel);
            Assert.That(sc.MaxLevel, Is.EqualTo(9));
        }
    }
}