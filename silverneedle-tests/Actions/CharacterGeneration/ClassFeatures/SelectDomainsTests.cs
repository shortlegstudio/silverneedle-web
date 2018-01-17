// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.ClassFeatures
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Domains;
    using SilverNeedle.Serialization;

    
    public class SelectDomainsTests : RequiresDataFiles
    {
        [Fact]
        public void ChoosesANumberOfDomainsBasedOnConfiguration()
        {
            var configure = new MemoryStore();
            configure.SetValue("count", "2");
            var selectDomains = new SelectDomains(configure);

            var character = CharacterTestTemplates.Cleric();
            selectDomains.ExecuteStep(character);
            var domains = character.GetAll<Domain>();

            Assert.Equal(domains.Count(), 2);
            Assert.NotEqual(domains.ElementAt(0), domains.ElementAt(1));
        }
    }
}