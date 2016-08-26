using NUnit.Framework;
using SilverNeedle.Characters;
using System.Linq;
using System.Xml.Linq;

namespace Characters {

	[TestFixture]
	public class SizeStatsTests {

		[Test]
		public void ASmallCreatureAsASizeModifierOfOne() {
			var smallCreature = new SizeStats (CharacterSize.Small, 36, 36);
			Assert.AreEqual (1, smallCreature.SizeModifier);
		}

		[Test]
		public void UpdatingTheSizeChangesTheModifier() {
			var smallToStart = new SizeStats (CharacterSize.Small, 32, 28);
			smallToStart.SetSize (CharacterSize.Large, 89, 283);
			Assert.AreEqual (-1, smallToStart.SizeModifier);
		}

		[Test]
		public void ContainsModifiersForFlyAndStealth() {
			var medium = new SizeStats (CharacterSize.Medium, 70, 184);
			Assert.IsTrue(medium.Modifiers.Any(x => x.StatisticName == "Stealth"));
			Assert.IsTrue (medium.Modifiers.Any (x => x.StatisticName == "Fly"));	
		}

		[Test]
		public void SmallCreaturesProvideABonusToStealthAndFly() {
			var small = new SizeStats (CharacterSize.Small, 34, 37);
			var stealth = small.Modifiers.First (x => x.StatisticName == "Stealth");
			Assert.AreEqual (4, stealth.Modifier);
		}

		[Test]
		public void ColossalCreaturesAreBadAtStealth() {
			var col = new SizeStats (CharacterSize.Colossal, 680, 29932);
			var stealth = col.Modifiers.First (x => x.StatisticName == "Stealth");
			Assert.AreEqual (-16, stealth.Modifier);
		}

		[Test]
		public void FineCreaturesAreGoodAtFlying() {
			var fine = new SizeStats (CharacterSize.Fine, 1, 2);
			var fly = fine.Modifiers.First (x => x.StatisticName == "Fly");
			Assert.AreEqual (8, fly.Modifier);
		}

		[Test]
		public void LargeCreaturesArePoorAtFlying() {
			var large = new SizeStats (CharacterSize.Large, 1, 3);
			var fly = large.Modifiers.First (x => x.StatisticName == "Fly");
			Assert.AreEqual (-2, fly.Modifier);
		}

		[Test]
		public void DefaultStatsAreMedium() {
			var def = new SizeStats ();
			Assert.AreEqual (CharacterSize.Medium, def.Size);
			Assert.AreEqual (72.0f, def.Height);
			Assert.AreEqual (180f, def.Weight);
		}
	}
}
