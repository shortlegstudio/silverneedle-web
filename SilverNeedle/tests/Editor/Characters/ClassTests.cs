using NUnit.Framework;
using SilverNeedle.Characters;


namespace Characters {

	[TestFixture]
	public class ClassTests {
		[Test]
        public void GetLevelReturnsEmptyLevelIfNothingIsThere()
        {
            var c = new Class();
            var l = c.GetLevel(10);
            Assert.IsNotNull(l);
            Assert.IsInstanceOf(typeof(Level), l);
        }

	}
}