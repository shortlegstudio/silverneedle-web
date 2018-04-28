// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;
    public class SlowFall : IAbility, IComponent
    {
        public ComponentContainer Parent { get; set; }
        private DataTable monkAbilities;
        private ClassLevel monkLevel;
        public SlowFall(DataTable abilities)
        {
            monkAbilities = abilities;
        }

        public SlowFall()
        {
            monkAbilities = Serialization.GatewayProvider.Find<DataTable>("Monk Abilities");
        }
        public void Initialize(ComponentContainer components)
        {
            monkLevel = components.Get<ClassLevel>();
        }

        private string DistanceValue
        { 
            get 
            { 
                return monkAbilities.Get(monkLevel.Level.ToString(), "slow-fall");
            }
        }

        public string DisplayString()
        {
            return "Slow Fall ({0} ft)".Formatted(DistanceValue);
        }
    }
}