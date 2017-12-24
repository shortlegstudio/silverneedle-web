// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class DazingTouch : SpecialAbility, IComponent
    {
        private AbilityScore baseAbility;
        private AbilityScoreTypes baseAbilityType;
        public int UsesPerDay
        {
            get { return 3 + baseAbility.TotalModifier; }
        }

        public DazingTouch(AbilityScore baseAbility)
        {
            this.baseAbility = baseAbility;
        }

        public DazingTouch(IObjectStore configuration)
        { 
            baseAbilityType = configuration.GetEnum<AbilityScoreTypes>("base-ability");
        }

        public void Initialize(ComponentContainer components)
        {
            if(baseAbility == null)
            {
                baseAbility = components.Get<AbilityScores>().GetAbility(baseAbilityType);
            }
        }
    }
}