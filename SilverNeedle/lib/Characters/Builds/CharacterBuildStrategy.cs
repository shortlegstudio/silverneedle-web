// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

namespace SilverNeedle.Characters
{
    using SilverNeedle.Utility;

    public class CharacterBuildStrategy : IGatewayObject
    {
        public CharacterBuildStrategy()
        {
            Classes = new WeightedOptionTable<string>();
            Races = new WeightedOptionTable<string>();
            FavoredSkills = new WeightedOptionTable<string>();
            FavoredFeats = new WeightedOptionTable<string>();
            FavoredAbilities = new WeightedOptionTable<AbilityScoreTypes>();
            FillInMissingAbilities(FavoredAbilities);
        }

        public CharacterBuildStrategy(IObjectStore data)
        {
            Classes = new WeightedOptionTable<string>();
            Races = new WeightedOptionTable<string>();
            FavoredSkills = new WeightedOptionTable<string>();
            FavoredFeats = new WeightedOptionTable<string>();
            FavoredAbilities = new WeightedOptionTable<AbilityScoreTypes>();
            ParseObjectStore(data);                
        }
        
        public string Name { get; set; }
        public WeightedOptionTable<string> Classes { get; set; }
        public WeightedOptionTable<string> Races { get; set; }

        public float ClassSkillMultiplier { get; set; }

        public int BaseSkillWeight { get; set; }

        public WeightedOptionTable<string> FavoredSkills { get; set; }
        public WeightedOptionTable<string> FavoredFeats { get; set; }

        public WeightedOptionTable<AbilityScoreTypes> FavoredAbilities { get; set; }

        private void ParseObjectStore(IObjectStore data)
        {
            // Basic Properties
                Name = data.GetString("name");
                ClassSkillMultiplier = data.GetFloat("classskillmultiplier");
                BaseSkillWeight = data.GetInteger("baseskillweight");
                
                // Collections
                //
                // Races
                var races = data.GetObject("races");
                BuildWeightedTable(Races, races);
                
                // Classes
                var classes = data.GetObject("classes");
                BuildWeightedTable(Classes, classes);
                
                // Skills
                var skills = data.GetObject("skills");
                BuildWeightedTable(FavoredSkills, skills);
                
                // Feats
                var feats = data.GetObject("feats");
                BuildWeightedTable(FavoredFeats, feats);

                var abilities = data.GetObjectOptional("abilities");
                BuildAbilityTable(FavoredAbilities, abilities);
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

        public bool Matches(string name)
        {
            return Name.EqualsIgnoreCase(name);
        }
    }
}