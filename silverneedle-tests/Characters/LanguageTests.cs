// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters {
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

	
	public class LanguageTests
	{
		[Fact]
		public void ParseTheYamlFile() 
        {
            var list = LanguageYamlFile.ParseYaml ().Load<Language>();
			var french = list.First (x => x.Name == "French");
			Assert.Equal ("C'est la vie", french.Description);
			var english = list.First (x => x.Name == "English");
			Assert.Equal ("Good day old boy", english.Description);
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
