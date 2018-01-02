// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class CharacterStrategy : IGatewayObject, IGatewayCopy<CharacterStrategy>
    {

        public static CharacterStrategy Default()
        {
            return new CharacterStrategy();
        }
        public CharacterStrategy()
        {
            BuildAlignmentTable();
            FillInMissingAbilities(FavoredAbilities);
            TargetLevel = 1;
            QuirkCount = 3;
            FearCount = 1;
            FavoriteColorCount = 3;
            this.AbilityScoreRoller = typeof(SilverNeedle.Actions.CharacterGeneration.Abilities.AverageAbilityScoreGenerator).FullName;
        }

        public CharacterStrategy(IObjectStore data) 
        {
            FavoriteColorCount = 3;
            QuirkCount = 3;
            FearCount = 1;
            TargetLevel = 1;
            ParseObjectStore(data);
        }

        public CharacterStrategy(CharacterStrategy copy)
        {
            this.Name = copy.Name;
            this.TargetLevel = copy.TargetLevel;
            this.Designer = copy.Designer;
            this.BaseSkillWeight = copy.BaseSkillWeight;
            this.ClassSkillMultiplier = copy.ClassSkillMultiplier;
            this.QuirkCount = copy.QuirkCount;
            this.FearCount = copy.FearCount;
            this.FavoriteColorCount = copy.FavoriteColorCount;
            this.AbilityScoreRoller = copy.AbilityScoreRoller;
            this.AddLanguageChoices(copy.LanguageChoices);
            this.AddLanguagesKnown(copy.LanguagesKnown);
            foreach(var table in copy.customTables)
            {
                this.customTables.Add(table.Key, table.Value.Copy());
            }
        }
        
        public string Name { get; set; }
        public WeightedOptionTable<string> Classes { get { return GetCustomTable<string>("classes"); } }
        public WeightedOptionTable<string> Races { get { return GetCustomTable<string>("races"); } }
        public WeightedOptionTable<string> FavoredSkills { get { return GetCustomTable<string>("favored-skills"); } }
        public WeightedOptionTable<string> FavoredFeats { get { return GetCustomTable<string>("favored-feats"); } }
        public WeightedOptionTable<AbilityScoreTypes> FavoredAbilities { get { return GetCustomTable<AbilityScoreTypes>("favored-abilities"); } }
        public WeightedOptionTable<CharacterAlignment> FavoredAlignments { get { return GetCustomTable<CharacterAlignment>("favored-alignments"); } }
        public IEnumerable<string> LanguageChoices { get { return this.languageChoiceList; } }
        public IEnumerable<string> LanguagesKnown { get { return this.languagesKnownList; } }

        public float ClassSkillMultiplier { get; set; }
        public int BaseSkillWeight { get; set; }
        public string Designer { get; set; }
        public string AbilityScoreRoller { get; set; }

        public int TargetLevel { get; set; }
        public int QuirkCount { get; set; }
        public int FearCount { get; set; }
        public int FavoriteColorCount { get; set; }

        private IList<string> languageChoiceList = new List<string>();
        private IList<string> languagesKnownList = new List<string>();
        private IDictionary<string, IWeightedOptionTable> customTables = new Dictionary<string, IWeightedOptionTable>();
        public void AddLanguageKnown(string language) { this.languagesKnownList.Add(language); }
        public void AddLanguageChoice(string language) { this.languageChoiceList.Add(language); }
        public void AddLanguagesKnown(IEnumerable<string> languages) { this.languagesKnownList.Add(languages); }
        public void AddLanguageChoices(IEnumerable<string> languages) { this.languageChoiceList.Add(languages); }
        private void ParseObjectStore(IObjectStore data)
        {
            // Basic Properties
            Name = data.GetString("name");
            ClassSkillMultiplier = data.GetFloat("classskillmultiplier");
            BaseSkillWeight = data.GetInteger("baseskillweight");
            AbilityScoreRoller = data.GetStringOptional("ability-score-roller");
            if (string.IsNullOrEmpty(AbilityScoreRoller)) 
            {
                this.AbilityScoreRoller = typeof(SilverNeedle.Actions.CharacterGeneration.Abilities.AverageAbilityScoreGenerator).FullName;
            }
            
            var races = data.GetObject("races");
            BuildWeightedTable(Races, races);
            
            var classes = data.GetObject("classes");
            BuildWeightedTable(Classes, classes);
            
            var skills = data.GetObject("skills");
            BuildWeightedTable(FavoredSkills, skills);
            
            var feats = data.GetObject("feats");
            BuildWeightedTable(FavoredFeats, feats);

            var abilities = data.GetObjectOptional("abilities");
            BuildAbilityTable(FavoredAbilities, abilities);

            BuildAlignmentTable();
            Designer = data.GetStringOptional("designer");
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

        private void BuildAlignmentTable()
        {
            foreach(var a in EnumHelpers.GetValues<CharacterAlignment>())
            {
                if(!FavoredAlignments.HasOption(a))
                {
                    FavoredAlignments.AddEntry(a, 1);
                }
            }
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

        public CharacterStrategy Copy()
        {
            return new CharacterStrategy(this);
        }

        public T ChooseOption<T>(string tableName)
        {
            return GetCustomTable<T>(tableName).ChooseRandomly();
        }

        public IEnumerable<T> GetOptions<T>(string tableName)
        {
            return GetCustomTable<T>(tableName).UniqueList();
        }

        public void AddCustomValue<T>(string tableName, T value, int weight)
        {
            var table = GetCustomTable<T>(tableName);
            if(table == null)
            {
            }
            table.AddEntry(value, weight);
        }

        private WeightedOptionTable<T> GetCustomTable<T>(string tableName)
        {
            if(customTables.ContainsKey(tableName) == false)
            {
                customTables[tableName] = new WeightedOptionTable<T>();
            }

            return customTables[tableName] as WeightedOptionTable<T>;
        }
    }
}