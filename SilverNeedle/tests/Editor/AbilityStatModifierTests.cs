using NUnit.Framework;
using System.Linq;
using SilverNeedle;
using SilverNeedle.Characters;

[TestFixture]
public class AbilityStatModifierTests
{
    [Test]
    public void AbilityStatModifiersTrackChangesToAbility()
    {
        var ability = new AbilityScore();
        ability.SetValue(10);

        var modifier = new AbilityStatModifier(ability);
        Assert.AreEqual(0, modifier.Modifier);
        ability.SetValue(20);
        Assert.AreEqual(05, modifier.Modifier);
    }
}
