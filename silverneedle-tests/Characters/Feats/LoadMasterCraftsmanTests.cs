// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Feats
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Feats;
    using SilverNeedle.Serialization;
    using Xunit;

    public class LoadMasterCraftsmanTests : RequiresDataFiles
    {
        [Fact]
        public void CreatesAFeatForEachCraftAndProfessionSkill()
        {
            var yaml = @"---
loader-template: |
  name: Master Craftsman ({0})
  prerequisites:
    - skillranks:
      name: {0}
      minimum: 5";
            var loader = new LoadMasterCraftsman();
            var results = loader.Load(yaml.ParseYaml());
            Assert.NotEmpty(results);
            foreach(var r in results)
            {
                Assert.False(r.Name.Contains("{0}")); 
                Assert.True(r.Prerequisites.Count > 0);
            }
        }
    }
}