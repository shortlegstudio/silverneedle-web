// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Utility;
    public class KiPool : SpecialAbility, IComponent
    {
        private ComponentContainer components;
        public KiPool()
        {
            KiPoints = new BasicStat("Ki Points", 0);
        }
        public void Initialize(ComponentContainer components)
        {
            var wisdom = components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom);
            KiPoints.AddModifier(wisdom.UniversalStatModifier);

            var monk = components.Get<ClassLevel>();
            KiPoints.AddModifier(
                new DelegateStatModifier("Ki Points",
                    "Monk Levels", "Monk",
                    () => { return monk.Level / 2; }
                )
            );

            this.components = components;
        }

        public IStatistic KiPoints  { get; private set; }

        public override string Name
        {
            get
            {
                return string.Format("Ki Pool ({0} points {1})",
                    this.KiPoints.TotalValue,
                    string.Join(", ", GetKiStrikes().Select(x => x.DamageType))
                );
            }
        }

        private IEnumerable<KiStrike> GetKiStrikes()
        {
            return components.GetAll<KiStrike>();
        }

    }
}