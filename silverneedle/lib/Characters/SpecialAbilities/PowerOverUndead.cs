// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;

    public class PowerOverUndead : AbilityDisplayAsName, IComponent
    {
        public ComponentContainer Parent { get; set; }
        public void Initialize(ComponentContainer components)
        {
            var alignment = components.Get<CharacterAlignment>();
            var good = new FeatToken("Turn Undead", true);
            var evil = new FeatToken("Command Undead", true);

            if(alignment.IsGood())
            {
                components.Add(good);
            }
            else if(alignment.IsEvil())
            {
                components.Add(evil);
            }
            else
            {
                components.Add(new FeatToken[] { good, evil }.ChooseOne());
            }
        }
    }
}