// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;
    public class CelestialArcana : BloodlineArcana, IComponent
    {
        private ClassLevel sorcererLevel;

        public override string BonusAbility 
        { 
            get { return "summoned creatures gain DR {0}/evil".Formatted(sorcererLevel.Level/2); } 
        }

        public void Initialize(ComponentContainer components)
        {
            sorcererLevel = components.Get<ClassLevel>();
        }
    }
}