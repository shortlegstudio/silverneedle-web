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
                var build = new CharacterBuildStrategy();

                // Basic Properties
                build.Name = node.GetString("name");
                build.ClassSkillMultiplier = node.GetFloat("classskillmultiplier");
                build.BaseSkillWeight = node.GetInteger("baseskillweight");
                
                // Collections
                //
                // Races
                var races = node.GetObject("races");
                BuildWeightedTable(build.Races, races);
                
                // Classes
                var classes = node.GetObject("classes");
                BuildWeightedTable(build.Classes, classes);
                
                // Skills
                var skills = node.GetObject("skills");
                BuildWeightedTable(build.FavoredSkills, skills);
                
                // Feats
                var feats = node.GetObject("feats");
                BuildWeightedTable(build.FavoredFeats, feats);

                var abilities = node.GetObjectOptional("abilities");
                BuildAbilityTable(build.FavoredAbilities, abilities);

                characterBuilds.Add(build);
            }
        }
        
        private void BuildWeightedTable(WeightedOptionTable<string> tableToBuild, IObjectStore node)
        {
            foreach(var child in node.Children)
            {
                tableToBuild.AddEntry(child.GetString("name"), child.GetInteger("weight"));
            }
        }  

        private void BuildAbilityTable(WeightedOptionTable<AbilityScoreTypes> abilityTable, IObjectStore node)
        {
            if (node != null)
            {
                foreach(var child in node.Children)
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