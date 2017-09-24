// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities;
    public class StillMindTests
    {
        [Fact]
        public void StillMindAddsSavingThrowBonusAgainstEnchantmentSpells()
        {
            var bob = CharacterTestTemplates.AverageBob();
            var stillMind = new StillMind();
            bob.Add(stillMind);
            var enchant = bob.Defense.FortitudeSave.GetConditionalValue("enchantment");
            var total = bob.Defense.FortitudeSave.TotalValue;
            Assert.Equal(total+2, enchant);
        }
    }
}