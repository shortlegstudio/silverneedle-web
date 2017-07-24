// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using System;
    using SilverNeedle.Utility;

    public class TrapFinding : SpecialAbility, IComponent
    {
        public ConditionalStatModifier PerceptionModifier { get; private set; }
        public ConditionalStatModifier DisableDeviceModifier { get; private set; }
        public void Initialize(ComponentBag components)
        {
            var rogueLevel = components.Get<ClassLevel>();
            PerceptionModifier = new ConditionalStatModifier(
                new DelegateStatModifier("Perception", "bonus", "traps", () => { return Math.Max(1, rogueLevel.Level / 2);})
                , "traps"
            );
            DisableDeviceModifier = new ConditionalStatModifier(
                new DelegateStatModifier("Disable Device", "bonus", "traps", () => { return Math.Max(1, rogueLevel.Level / 2);})
                , "traps"
            );

            var perception = components.Get<SkillRanks>().GetSkill(PerceptionModifier.StatisticName);
            perception.AddModifier(PerceptionModifier);
            var disable = components.Get<SkillRanks>().GetSkill(DisableDeviceModifier.StatisticName);
            disable.AddModifier(DisableDeviceModifier);
        }
    }
}