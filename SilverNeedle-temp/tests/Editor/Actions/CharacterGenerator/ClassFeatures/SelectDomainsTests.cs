// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.ClassFeatures
{
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Domains;
    using SilverNeedle.Serialization;

    [TestFixture]
    public class SelectDomainsTests
    {
        [Fact]
        public void ChoosesANumberOfDomainsBasedOnConfiguration()
        {
            var configure = new MemoryStore();
            configure.SetValue("count", "2");
            var selectDomains = new SelectDomains(configure);

            var character = Tests.Characters.CharacterSheetHelpers.CreateBlankStandardOGLSheet();
            selectDomains.Process(character, new CharacterBuildStrategy());
            var domains = character.GetAll<Domain>();

            Assert.That(domains.Count(), Is.EqualTo(2));
            Assert.That(domains.ElementAt(0), Is.Not.EqualTo(domains.ElementAt(1)));
        }
    }
}