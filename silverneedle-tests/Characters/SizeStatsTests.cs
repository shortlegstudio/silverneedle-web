// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters {
    using Xunit;
    using SilverNeedle.Characters;
    using System.Linq;

    
    public class SizeStatsTests {

        [Fact]
        public void ASmallCreatureAsASizeModifierOfOne() {
            var smallCreature = new SizeStats (CharacterSize.Small, 36, 36);
            Assert.Equal (1, smallCreature.PositiveSizeModifier.Modifier);
        }

        [Fact]
        public void UpdatingTheSizeChangesTheModifier() {
            var smallToStart = new SizeStats (CharacterSize.Small, 32, 28);
            smallToStart.SetSize (CharacterSize.Large, 89, 283);
            Assert.Equal (-1, smallToStart.PositiveSizeModifier.Modifier);
        }

        [Fact]
        public void ContainsModifiersForFlyAndStealth() {
            var medium = new SizeStats (CharacterSize.Medium, 70, 184);
            Assert.True(medium.Modifiers.Any(x => x.StatisticName == "Stealth"));
            Assert.True (medium.Modifiers.Any (x => x.StatisticName == "Fly"));	
        }

        [Fact]
        public void SmallCreaturesProvideABonusToStealthAndFly() {
            var small = new SizeStats (CharacterSize.Small, 34, 37);
            var stealth = small.Modifiers.First (x => x.StatisticName == "Stealth");
            Assert.Equal (4, stealth.Modifier);
        }

        [Fact]
        public void ColossalCreaturesAreBadAtStealth() {
            var col = new SizeStats (CharacterSize.Colossal, 680, 29932);
            var stealth = col.Modifiers.First (x => x.StatisticName == "Stealth");
            Assert.Equal (-16, stealth.Modifier);
        }

        [Fact]
        public void FineCreaturesAreGoodAtFlying() {
            var fine = new SizeStats (CharacterSize.Fine, 1, 2);
            var fly = fine.Modifiers.First (x => x.StatisticName == "Fly");
            Assert.Equal (8, fly.Modifier);
        }

        [Fact]
        public void LargeCreaturesArePoorAtFlying() {
            var large = new SizeStats (CharacterSize.Large, 1, 3);
            var fly = large.Modifiers.First (x => x.StatisticName == "Fly");
            Assert.Equal (-2, fly.Modifier);
        }

        [Fact]
        public void DefaultStatsAreMedium() {
            var def = new SizeStats ();
            Assert.Equal (CharacterSize.Medium, def.Size);
            Assert.Equal (72.0f, def.Height);
            Assert.Equal (180f, def.Weight);
        }
    }
}
