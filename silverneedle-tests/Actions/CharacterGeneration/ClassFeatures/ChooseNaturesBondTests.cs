// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.ClassFeatures
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;
    using SilverNeedle.Characters.Domains;

    public class ChooseNaturesBondTests
    {
        [Fact]
        public void SelectsFromALimitedListOfDomains()
        {
            var airDomain = DomainTemplates.AirDomain();

            var configure = new MemoryStore();
            configure.SetValue("domain-options", new string[] { "Air" });
            var chooser = new ChooseNaturesBond(configure, EntityGateway<Domain>.LoadWithSingleItem(airDomain));


            var donna = CharacterTestTemplates.DruidDonna();
            chooser.ExecuteStep(donna, new CharacterBuildStrategy());

            Assert.Equal(airDomain, donna.Get<Domain>());
        }
    }
}