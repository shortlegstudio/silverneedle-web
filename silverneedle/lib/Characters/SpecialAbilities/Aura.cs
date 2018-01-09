// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Serialization;
    
    public class Aura : IAbility 
    { 
        [ObjectStore("aura-type")]
        public string AuraType { get; private set; }    
        public Aura(IObjectStore data)
        {
            data.Deserialize(this);
        }

        public string DisplayString()
        {
            return "Aura of {0}".Formatted(this.AuraType);
        }
    }
}