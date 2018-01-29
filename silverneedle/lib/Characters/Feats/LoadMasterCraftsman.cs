// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.Feats
{
    using System.Collections.Generic;
    using SilverNeedle.Serialization;
    public class LoadMasterCraftsman : IGatewayLoader<Feat>
    {
        public IEnumerable<Feat> Load(IObjectStore configuration)
        {
            var craftsmanFeats = new List<Feat>();
            var skills = GatewayProvider.Get<Skill>().Where(x => x.IsProfessionSkill || x.IsCraftSkill);
            foreach(var skl in skills)
            {
                var yaml = configuration.GetString("loader-template");
                yaml = yaml.Formatted(skl.Name);
                var crft = new Feat(yaml.ParseYaml());
                craftsmanFeats.Add(crft);
            }

            return craftsmanFeats;
        }
    }
}