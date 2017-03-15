using NUnit.Framework;
using System.Linq;
using SilverNeedle;


[TestFixture]
public class BasicStatTests {

    [Test]
    public void StatsRaiseModifiedEventWhenValueSet() {
		var stat = new BasicStat (20);
		var changeCalled = false;
		stat.Modified += (object sender, BasicStatModifiedEventArgs e) => {
			changeCalled = true;
		};

		stat.SetValue (21);
		Assert.True (changeCalled);
    }

	[Test]
	public void StatsRaiseModifiedEventWhenAdjustmentAdded() {
		var stat = new BasicStat (20);
		var changeCalled = false;
		stat.Modified += (object sender, BasicStatModifiedEventArgs e) => {
			changeCalled = true;
		};
		stat.AddModifier (new BasicStatModifier ());

		Assert.True (changeCalled);
	}

	[Test]
	public void StatsRaiseTheOldValueAndNewValue() {
		var stat = new BasicStat (10);
		bool mod = false;
		stat.Modified += (object sender, BasicStatModifiedEventArgs e) => {
			Assert.AreEqual(10, e.OldBaseValue);
			Assert.AreEqual(14, e.NewBaseValue);
			mod = true;
		};
		stat.SetValue (14);

		Assert.IsTrue (mod);
	}

	[Test]
	public void StatsTotalUpAdjustments() {
		var stat = new BasicStat (10);
		stat.AddModifier (new BasicStatModifier (5, "Foo"));
		Assert.AreEqual (15, stat.TotalValue);
	}

	[Test]
	public void StatModifiersCanHaveConditionalModifiers() {
		var stat = new BasicStat(10);
		var mod = new ConditionalStatModifier("vs. Giants", "Skill", 5, "bonus", "Feat");
		stat.AddModifier(mod);
		Assert.AreEqual(10, stat.TotalValue);
		Assert.AreEqual(15, stat.GetConditionalValue("vs. Giants"));
		Assert.AreEqual(1, stat.GetConditions().Count());
		Assert.AreEqual("vs. Giants", stat.GetConditions().First());
	}

	[Test]
	public void StatsCanHaveAListOfConditionalModifiers() {
		var stat = new BasicStat(10);
		stat.AddModifier(
			new ConditionalStatModifier("vs. Corgis", "Skill", 3, "bonus", "Trait")
		);
		stat.AddModifier(
			new ConditionalStatModifier("vs. Corgis", "Skill", 3, "bonus", "Trait")
		);
		stat.AddModifier(
			new ConditionalStatModifier("Trapfinding", "Skill", 3, "bonus", "Trait")
		);

		Assert.AreEqual(2, stat.GetConditions().Count());
		Assert.IsTrue(stat.GetConditions().Any(x => x == "vs. Corgis"));
		Assert.IsTrue(stat.GetConditions().Any(x => x == "Trapfinding"));
	}

	[Test]
	public void CastingDoesntBreakConditionalModifiers() {
		var stat = new BasicStat(10);
		BasicStatModifier mod = new ConditionalStatModifier("vs. Thor", "Attack Bonus", 3, "bonus", "Food");
		stat.AddModifier(mod);
		Assert.AreEqual(1, stat.GetConditions().Count());
		Assert.AreEqual(10, stat.TotalValue);
		Assert.AreEqual(13, stat.GetConditionalValue("vs. Thor"));
	}
		
	[Test]
	public void FormatNiceStringVersionOfStat() {
		var stat = new BasicStat(20);
		BasicStatModifier mod = new ConditionalStatModifier("vs. Thor", "Attack Bonus", 3, "bonus", "Food");
		stat.AddModifier(mod);
		Assert.AreEqual("Fight +20 (+23 vs. Thor)", stat.ToString("Fight"));
	}

    [Test]
    public void AlwaysRoundDownStats() {
        var stat = new BasicStat(2);
        stat.AddModifier(new BasicStatModifier(-1, "Food"));
        stat.AddModifier(new BasicStatModifier(0.667f, "Because"));
        Assert.AreEqual(-1, stat.SumBasicModifiers);
        Assert.AreEqual(1, stat.TotalValue);
    }
}
