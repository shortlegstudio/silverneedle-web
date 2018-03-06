// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using SilverNeedle.Serialization;
    public class CharacterSheetSetup : Feature, IGatewayObject
    {
        public string Name { get; private set; }
        public CharacterSheetSetup(IObjectStore configuration) : base(configuration)
        {
            this.Name = configuration.GetString("name");
        }

        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }
    }
}