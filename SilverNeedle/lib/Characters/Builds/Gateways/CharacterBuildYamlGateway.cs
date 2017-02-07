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
        private string CharacterBuildDataFileType = "characterbuilds";

        private List<CharacterBuildStrategy> characterBuilds = new List<CharacterBuildStrategy>();

        public CharacterBuildYamlGateway()
        {
            // Use DatafileLoader to get all class files;
            var yamlNodes = DatafileLoader.Instance.GetYamlFiles(CharacterBuildDataFileType);
            foreach(var y in yamlNodes) {
                this.ParseYaml(y);
            }
        }

        public CharacterBuildYamlGateway(YamlNodeWrapper yamlNodeWrapper)
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

        private void ParseYaml(YamlNodeWrapper yaml)
        {
            foreach(var node in yaml.Children())
            {
                var build = new CharacterBuildStrategy();

                // Basic Properties
                build.Name = node.GetString("name");
                build.ClassSkillMultiplier = node.GetFloat("classskillmultiplier");
                build.BaseSkillWeight = node.GetInteger("baseskillweight");
                
                // Collections
                //
                // Races
                var races = node.GetNode("races");
                BuildWeightedTable(build.Races, races);
                
                // Classes
                var classes = node.GetNode("classes");
                BuildWeightedTable(build.Classes, classes);
                
                // Skills
                var skills = node.GetNode("skills");
                BuildWeightedTable(build.FavoredSkills, skills);
                
                // Feats
                var feats = node.GetNode("feats");
                BuildWeightedTable(build.FavoredFeats, feats);

                var abilities = node.GetNodeOptional("abilities");
                BuildAbilityTable(build.FavoredAbilities, abilities);

                characterBuilds.Add(build);
            }
        }
        
        private void BuildWeightedTable(WeightedOptionTable<string> tableToBuild, YamlNodeWrapper node)
        {
            foreach(var child in node.Children())
            {
                tableToBuild.AddEntry(child.GetString("name"), child.GetInteger("weight"));
            }
        }  

        private void BuildAbilityTable(WeightedOptionTable<AbilityScoreTypes> abilityTable, YamlNodeWrapper node)
        {
            if (node != null)
            {
                foreach(var child in node.Children())
                {
                    abilityTable.AddEntry(child.GetEnum<AbilityScoreTypes>("name"), child.GetInteger("weight"));
                }
            }

            FillInMissingAbilities(abilityTable);
        }

        private void FillInMissingAbilities(WeightedOptionTable<AbilityScoreTypes> abilityTable)
        {
            //build empty table
            foreach(var a in EnumHelpers.GetValues<AbilityScoreTypes>())
            {
                if(!abilityTable.HasOption(a)) 
                {
                    abilityTable.AddEntry(a, 1);
                }
            }
        }
    }
}