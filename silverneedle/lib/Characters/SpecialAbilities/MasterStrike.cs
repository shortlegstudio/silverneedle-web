// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Utility;
    public class MasterStrike : SpecialAbility, IComponent
    {
        public MasterStrikeAttack Attack { get; private set; }

        public void Initialize(ComponentBag components)
        {
            var rogueLevel = components.Get<ClassLevel>();
            var intelligence = components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Intelligence);
            Attack = new MasterStrikeAttack(rogueLevel, intelligence);
            components.Add(this.Attack);
        }

        public class MasterStrikeAttack : WeaponAttack
        {
            private ClassLevel rogueLevel;
            private AbilityScore intelligence;
            public MasterStrikeAttack(ClassLevel rogueLevel, AbilityScore intelligence)
            {
                this.Name = "Master Strike";
                this.AttackType = AttackTypes.Special;
                this.rogueLevel = rogueLevel;
                this.intelligence = intelligence;
            }

            public override int SaveDC
            {
                get
                {
                    return 10 + intelligence.TotalModifier + rogueLevel.Level / 2;
                }
            }


            public override string ToString()
            {
                return string.Format("{0} (DC {1})", this.Name, this.SaveDC);
            }
        }
    }
}