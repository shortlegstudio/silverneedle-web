// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System.Linq;
    using SilverNeedle.Serialization;
    using SilverNeedle.Lexicon;

    [ObjectStoreSerializable]
    public class Occupation : ILexiconGatewayObject
    {
        [ObjectStore("name")]
        public string Name { get; set; }
        [ObjectStore("class")]
        public string Class { get; set; }

        [ObjectStoreOptional("tags")]
        public string[] Tags { get; set; }

        [ObjectStoreOptional("skills")]
        public string[] Skills { get; set; }

        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }
        public bool MatchAnyTags(string [] tags)
        {
            if(tags == null)
                return false;
            return this.Tags.Any(x => tags.Contains(x));
        }

        public Occupation() 
        { 
            this.Tags = new string[] { };
        }
        public Occupation(string name, string characterClass, string[] tags)
        {
            this.Name = name;
            this.Class = characterClass;
            this.Tags = tags;
        }


        private static Occupation unemployed;
        public static Occupation Unemployed()
        {
            if(unemployed == null)
                unemployed = new Occupation("Unemployed", "", new string[] { });

            return unemployed;
        }

        public void AddSkillEntry(CharacterStrategy strategy, int weight)
        {
            if (Skills == null)
                return;
            
            foreach (var s in Skills)
            {
                if (!strategy.FavoredSkills.HasOption((s)))
                {
                    strategy.FavoredSkills.AddEntry(s, weight);
                }
                
            }
        }
        

    }
}