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

    public class IsSpellCasterTests
    {
        [Fact]
        public void IsQualifiedIfHasSpellCastingComponent()
        {
            var isSpellcaster = new IsSpellCaster();
            var bob = CharacterTestTemplates.AverageBob();
            Assert.False(isSpellcaster.IsQualified(bob));
            var sc = new Mock<ISpellCasting>();
            bob.Add(sc.Object);
            Assert.True(isSpellcaster.IsQualified(bob));
        }
    }
}