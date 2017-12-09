// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities;

    public class PurityOfBodyTests
    {
        [Fact]
        public void ProvidesImmunityToDisease()
        {
            var monk = CharacterTestTemplates.MarkyMonk();
            monk.Add(new PurityOfBody());
            AssertCharacter.IsImmuneTo("disease", monk);
        }
    }
}