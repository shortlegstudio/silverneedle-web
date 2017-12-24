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

    public class War : Domain, IComponent, IImprovesWithLevels
    {
        private ClassLevel source;
        private BattleRage battleRage;
        private WeaponMaster weaponMaster;
        public War(IObjectStore data) : base(data)
        {
            
        }

        public void Initialize(ComponentContainer components)
        {
            source = components.Get<ClassLevel>();
            battleRage = new BattleRage(components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom));
            components.Add(battleRage);
        }

        public void LeveledUp(ComponentContainer components)
        {
            if(source.Level == 8)
            {
                weaponMaster = new WeaponMaster();
                components.Add(weaponMaster);
            }
        }
    }
}