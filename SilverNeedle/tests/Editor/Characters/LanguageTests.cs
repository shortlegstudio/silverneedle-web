using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using SilverNeedle.Characters;
using SilverNeedle.Yaml;

namespace Tests.Characters {
	[TestFixture]
	public class LanguageTests
	{
		[Test]
		public void ParseTheYamlFile() 
        {
            var list = LanguageYamlFile.ParseYaml ().Load<Language>();
			var french = list.First (x => x.Name == "French");
			Assert.AreEqual ("C'est la vie", french.Description);
			var english = list.First (x => x.Name == "English");
			Assert.AreEqual ("Good day old boy", english.Description);
		}

		private const string LanguageYamlFile = @"--- 
- language: 
  name: French
  description: C'est la vie
- language:
  name: English
  description: Good day old boy
";
	}
}
