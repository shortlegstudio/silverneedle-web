// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Domains
{
    using System;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class Plant : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel source;
        private WoodenFist wooden;
        private BrambleArmor brambleArmor;
        public Plant(IObjectStore data) : base(data)
        {
            
        }

        public void Initialize(ComponentBag components)
        {
            source = components.Get<ClassLevel>();
            wooden = new WoodenFist(components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom));
            components.Add(wooden);
        }

        public void LeveledUp(ComponentBag components)
        {
            if(source.Level == 6)
            {
                brambleArmor = new BrambleArmor();
                components.Add(brambleArmor);
            }
        }
    }
}