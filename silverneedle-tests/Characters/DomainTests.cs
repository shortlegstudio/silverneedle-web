// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Domains;
    using SilverNeedle.Serialization;
    using Xunit;

    public class DomainTests : RequiresDataFiles
    {
        [Fact]
        public void TestAllDomainsLoadAndAddProperly()
        {
            var domains = GatewayProvider.All<Domain>();
            foreach(var d in domains)
            {
                var character = CharacterTestTemplates.Cleric();
                character.Add(d);
            }
        }
    }

}