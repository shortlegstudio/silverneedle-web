// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;
    public class AbyssalArcana : BloodlineArcana, IComponent
    {
        public ComponentContainer Parent { get; set; }
        private ClassLevel sorcererLevel;

        public override string BonusAbility 
        { 
            get { return "summoned creatures gain DR {0}/good".Formatted(sorcererLevel.Level/2); } 
        }
        public void Initialize(ComponentContainer components)
        {
            sorcererLevel = components.Get<ClassLevel>();
        }
    }
}