// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Domains
{
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;
    public class Good : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel source;
        private TouchOfGood touchOfGood;
        private HolyLance holyLance;
        public Good(IObjectStore data) : base(data)
        {
            
        }

        public void Initialize(ComponentBag components)
        {
            this.source = components.Get<ClassLevel>();
            touchOfGood = new TouchOfGood(components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom));
            components.Add(touchOfGood);
        }

        public void LeveledUp(ComponentBag components)
        {
            if(source.Level == 8)
            {
                holyLance = new HolyLance(source);
                components.Add(holyLance);
            }
        }
    }
}