// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using System.Linq;
    using Yaml;

    public class CharacterBuildYamlGateway : ICharacterBuildGateway
    {
        private List<CharacterBuild> characterBuilds = new List<CharacterBuild>();

        public CharacterBuildYamlGateway(YamlNodeWrapper yamlNodeWrapper)
        {
            ParseYaml(yamlNodeWrapper);
        }

        public IEnumerable<CharacterBuild> All() 
        {
            return characterBuilds;
        }

        public CharacterBuild GetBuild(string build)
        {
            return characterBuilds.FirstOrDefault(x => x.Name == build);
        }

        private void ParseYaml(YamlNodeWrapper yaml)
        {
            foreach(var node in yaml.Children())
            {
                var build = new CharacterBuild();
                build.Name = node.GetString("name");
                var races = node.GetNode("races");
                foreach(var r in races.Children())
                {
                    build.Races.AddEntry(r.GetString("name"), r.GetInteger("weight"));
                }
                var classes = node.GetNode("classes");
                foreach(var c in classes.Children())
                {
                    build.Classes.AddEntry(c.GetString("name"), c.GetInteger("weight"));
                }
                characterBuilds.Add(build);
            }

        }
    }
}