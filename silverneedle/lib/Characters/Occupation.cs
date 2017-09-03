// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
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

        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }
    }
}