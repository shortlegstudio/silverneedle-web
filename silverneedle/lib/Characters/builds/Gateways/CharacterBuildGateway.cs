// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Utility;

    public class CharacterBuildGateway : ICharacterBuildGateway
    {
        private string CharacterBuildDataFileType = "characterbuilds";

        private List<CharacterBuildStrategy> characterBuilds = new List<CharacterBuildStrategy>();

        public CharacterBuildGateway()
        {
            // Use DatafileLoader to get all class files;
            var yamlNodes = DatafileLoader.Instance.GetDataFiles(CharacterBuildDataFileType);
            foreach(var y in yamlNodes) {
                this.ParseYaml(y);
            }
        }

        public CharacterBuildGateway(IObjectStore yamlNodeWrapper)
        {
            ParseYaml(yamlNodeWrapper);
        }

        public IEnumerable<CharacterBuildStrategy> All() 
        {
            return characterBuilds;
        }

        public CharacterBuildStrategy GetBuild(string build)
        {
            return characterBuilds.FirstOrDefault(x => x.Name.EqualsIgnoreCase(build));
        }

        private void ParseYaml(IObjectStore yaml)
        {
            foreach(var node in yaml.Children)
            {
                var build = new CharacterBuildStrategy(node);
                characterBuilds.Add(build);
            }
        }
    }
}