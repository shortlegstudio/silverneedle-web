// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Utility;

    public class WellVersed : SpecialAbility, IComponent
    {
        private ConditionalStatModifier sonicModifier;
        private ConditionalStatModifier bardicPerformanceModifier;
        private ConditionalStatModifier languageDependentModifier;

        public void Initialize(ComponentBag components)
        {
            sonicModifier = new ConditionalStatModifier(new ValueStatModifier(4, "bonus"), "sonic");
            bardicPerformanceModifier = new ConditionalStatModifier(new ValueStatModifier(4, "bonus"), "bardic performance");
            languageDependentModifier = new ConditionalStatModifier(new ValueStatModifier(4, "bonus"), "language-dependent");
            var defense = components.Get<DefenseStats>();
            defense.FortitudeSave.AddModifier(sonicModifier);
            defense.FortitudeSave.AddModifier(bardicPerformanceModifier);
            defense.FortitudeSave.AddModifier(languageDependentModifier);
            defense.ReflexSave.AddModifier(sonicModifier);
            defense.ReflexSave.AddModifier(bardicPerformanceModifier);
            defense.ReflexSave.AddModifier(languageDependentModifier);
            defense.WillSave.AddModifier(sonicModifier);
            defense.WillSave.AddModifier(bardicPerformanceModifier);
            defense.WillSave.AddModifier(languageDependentModifier);
        }
    }
}