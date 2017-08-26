// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Treasure
{
    using SilverNeedle.Serialization;
    using SilverNeedle.Lexicon;

    [ObjectStoreSerializable]
    public class Gem : ILexiconGatewayObject
    {
        [ObjectStore("name")]
        public string Name { get; set; }


        public bool Matches(string name)
        {
            return Name.EqualsIgnoreCase(name);
        }
    }
}