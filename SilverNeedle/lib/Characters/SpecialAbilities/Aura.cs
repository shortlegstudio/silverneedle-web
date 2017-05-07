// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Serialization;
    
    public class Aura : SpecialAbility { 
        public string AuraType { get; private set; }    
        public Aura(IObjectStore data)
        {
            this.AuraType = data.GetString("type");
            this.Name = string.Format("Aura of {0}", this.AuraType);
        }
    }
}