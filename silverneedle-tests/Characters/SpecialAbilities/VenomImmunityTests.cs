// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    public class VenomImmunityTests
    {
        [Fact]
        public void AddImmunityToPoisons()
        {
            var bob = CharacterTestTemplates.AverageBob();
            var venom = new VenomImmunity();
            bob.Add(venom);
            Assert.Equal(new string[] { "poison" }, bob.Defense.Immunities.Select(imm => imm.Condition));
        }
    }

}