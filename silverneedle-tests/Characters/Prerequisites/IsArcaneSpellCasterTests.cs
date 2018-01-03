// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Prerequisites
{
    using Xunit;
    using Moq;
    using SilverNeedle.Characters.Prerequisites;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Spells;

    public class IsArcaneSpellCasterTests
    {
        [Fact]
        public void IsQualifiedIfHasSpellCastingComponentThatCastsArcaneSpells()
        {
            var isSpellcaster = new IsArcaneSpellCaster();
            var bob = CharacterTestTemplates.AverageBob();
            Assert.False(isSpellcaster.IsQualified(bob.Components));
            var sc = new Mock<ISpellCasting>();
            sc.SetupGet(p => p.SpellType).Returns(SpellType.Arcane);
            bob.Add(sc.Object);
            Assert.True(isSpellcaster.IsQualified(bob.Components));
        }

        [Fact]
        public void DivineCastersDoNotQualify()
        {
            var isSpellcaster = new IsArcaneSpellCaster();
            var bob = CharacterTestTemplates.AverageBob();
            var sc = new Mock<ISpellCasting>();
            sc.SetupGet(p => p.SpellType).Returns(SpellType.Divine);
            bob.Add(sc.Object);
            Assert.False(isSpellcaster.IsQualified(bob.Components));
        }
    }
}
