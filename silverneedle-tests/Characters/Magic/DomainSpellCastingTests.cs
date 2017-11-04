// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Magic
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;

    
    public class DomainSpellCastingTests
    {
        [Fact]
        public void ShouldReturnOneForSpellsPerDayIfAvailableAtThisLevel()
        {
            var classLevel = new ClassLevel(new Class());
            classLevel.Level = 5;
            var sc = new DomainSpellCasting(classLevel);
            Assert.Equal(sc.GetSpellsPerDay(1), 1);
            Assert.Equal(sc.GetSpellsPerDay(2), 1);
            Assert.Equal(sc.GetSpellsPerDay(3), 1);
            Assert.Equal(sc.GetSpellsPerDay(4), 0);
            
        }

        [Fact]
        public void ShouldReturnZeroForSpellsPerDayOfOrisons()
        {
            var classLevel = new ClassLevel(new Class());
            classLevel.Level = 5;
            var sc = new DomainSpellCasting(classLevel);
            Assert.Equal(sc.GetSpellsPerDay(0), 0);
        }

        [Fact]
        public void MaxLevelIsSpecifiedByClassLevel()
        {
            var classLevel = new ClassLevel(new Class());
            classLevel.Level = 5;
            var sc = new DomainSpellCasting(classLevel);
            Assert.Equal(sc.MaxLevel, 3);
        }

        [Fact]
        public void IfCharacterLevelIs19Or20Just()
        {
            var classLevel = new ClassLevel(new Class());
            classLevel.Level = 20;
            var sc = new DomainSpellCasting(classLevel);
            Assert.Equal(sc.MaxLevel, 9);
        }
    }
}