// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using System;
    using SilverNeedle.Characters;
    using SilverNeedle.Utility;

    public class DivineGrace : SpecialAbility, IComponent
    {
        public PositiveAbilityStatModifier SaveBonus { get ; private set; }
        public void Initialize(ComponentBag components)
        {
            var abilityScores = components.Get<AbilityScores>();
            var defense = components.Get<DefenseStats>();
            var charisma = abilityScores.GetAbility(AbilityScoreTypes.Charisma);
            SaveBonus = new PositiveAbilityStatModifier(charisma);
            defense.FortitudeSave.AddModifier(SaveBonus);
            defense.ReflexSave.AddModifier(SaveBonus);
            defense.WillSave.AddModifier(SaveBonus);
        }
    }
}