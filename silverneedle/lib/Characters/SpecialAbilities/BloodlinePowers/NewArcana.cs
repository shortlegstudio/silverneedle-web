// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Utility;

    public class NewArcana : SpecialAbility, IBloodlinePower, IImprovesWithLevels, IComponent
    {
        public void LeveledUp(ComponentBag components)
        {
            var clsLevel = components.Get<ClassLevel>();
            if(clsLevel.Level == 13 || clsLevel.Level == 17)
            {
                components.Add(LearnSpellToken.FromList("sorcerer-wizard"));
            }
        }

        public void Initialize(ComponentBag components)
        {
            components.Add(LearnSpellToken.FromList("sorcerer-wizard"));
        }
    }
}