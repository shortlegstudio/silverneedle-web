// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration
{
    using System.Collections.Generic;
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration;
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Shops;
    using SilverNeedle.Spells;

    public class PurchaseMagicalGearTests
    {
        PurchaseMagicalGear subject;

        public PurchaseMagicalGearTests()
        {
            var wands = new List<IGear>();
            wands.Add(new Wand(new Spell("Cure Light Wounds", "healing"), 20, 1));
            wands.Add(new Wand(new Spell("Lightning Bolt", "evocation"), 20, 1));
            var magicShop = new MagicShop(wands);
            
            subject = new PurchaseMagicalGear(magicShop);
        }

        [Fact]
        public void PurchasesWands()
        {
            var character = CharacterTestTemplates.AverageBob();
            
            character.Inventory.CoinPurse.SetValue(50000);

            subject.ExecuteStep(character);
            var wands = character.Inventory.GearOfType<IWand>();
            Assert.NotEmpty(wands);
        }
    }
}