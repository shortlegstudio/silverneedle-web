// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Lexicon
{
    using System.Collections.Generic;
    using SilverNeedle.Serialization;

    [ObjectStoreSerializable]
    public class Descriptor : IGatewayObject
    {
        [ObjectStore("name")]
        public string Name { get; set; }
        [ObjectStore("words")]
        public string[] Words { get; set; }

        public Descriptor() { }
        public Descriptor(string name, string[] words)
        {
            this.Name = name;
            this.Words = words;
        }

        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }

        public static string FindAndChooseWord(string descriptorName)
        {
            var descriptor = GatewayProvider.Find<Descriptor>(descriptorName);
            return descriptor.Words.ChooseOne();
        }
    }
}