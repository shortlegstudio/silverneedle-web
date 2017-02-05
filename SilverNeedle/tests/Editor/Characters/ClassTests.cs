using System.Linq;
using System.IO;
using NUnit.Framework;
using SilverNeedle;
using SilverNeedle.Dice;
using SilverNeedle.Characters;
using YamlDotNet.RepresentationModel;
using System.Text;


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