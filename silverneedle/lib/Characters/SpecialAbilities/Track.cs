// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;
    public class Track : SpecialAbility, IComponent
    {
        private ClassLevel rangerLevel;
        private ConditionalStatModifier trackingModifier;
        public void Initialize(ComponentContainer components)
        {
            rangerLevel = components.Get<ClassLevel>();
            trackingModifier = new ConditionalStatModifier(
                new DelegateStatModifier("Survival", "bonus", "Track", () => { return rangerLevel.Level / 2;})
                , "track"
            );
            var survivalSkill = components.Get<SkillRanks>().GetSkill("Survival");
            survivalSkill.AddModifier(trackingModifier);
        }

        public override string Name 
        {
            get { return string.Format("Track {0}", trackingModifier.Modifier.ToModifierString()); }
        }
    }
}