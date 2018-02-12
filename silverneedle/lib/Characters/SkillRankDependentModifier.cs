// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class SkillRankDependentModifier : IValueStatModifier, IComponent
    {
        private CharacterSkill _characterSkill;
        public float Modifier { get { return _characterSkill.Ranks < MinimumRanks ? 0 : ModifierWhenAvailable; } }

        [ObjectStore("modifier")]
        public float ModifierWhenAvailable { get; private set; }

        [ObjectStore("modifier-type")]
        public string ModifierType { get; private set; }

        [ObjectStoreOptional("condition")]
        public string Condition { get; private set; }

        [ObjectStore("name")]
        public string StatisticName { get; private set; }

        [ObjectStoreOptional("statistic-type")]
        public string StatisticType { get; private set; }

        [ObjectStore("skill")]
        public string Skill { get; private set; }

        [ObjectStore("minimum-ranks")]
        public int MinimumRanks { get; private set; }

        public void Initialize(ComponentContainer components)
        {
            _characterSkill = components.Get<SkillRanks>().GetSkill(Skill);
        }

        public SkillRankDependentModifier(IObjectStore configuration)
        {
            configuration.Deserialize(this);
        }
    }
}