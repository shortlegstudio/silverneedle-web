// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Attacks
{
    using SilverNeedle.Dice;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class LightningArc : IAttack, IComponent, IUsesPerDay, INameByType
    {
        public ComponentContainer Parent { get; set; }
        private ClassLevel clericLevel;
        private AbilityScore wisdom;
        public LightningArc(IObjectStore configuration)
        {
            configuration.Deserialize(this);
        }

        public void Initialize(ComponentContainer components)
        {
            clericLevel = components.Get<ClassLevel>();
            wisdom = components.Get<AbilityScores>().GetAbility(AbilityScoreTypes.Wisdom);
            this.DamageType = "electricity";
        }

        public Cup Damage
        {
            get
            {
                var cup = new Cup(Die.D6());
                cup.Modifier = (clericLevel.Level / 2).AtLeast(1);
                return cup;
            }
        }

        public int UsesPerDay
        {
            get
            {
                return 3 + wisdom.TotalModifier;
            }
        }

        public string Name { get { return this.Name(); } }
        public string DamageType { get; private set; }
        [ObjectStore("range")]
        public int Range { get; private set; }

        public AttackTypes AttackType { get { return AttackTypes.Special; } }

        public string DisplayString()
        {
            return string.Format("Lightning Arc {0}' ({1} {2})", this.Range.ToString(), this.Damage.ToString(), this.DamageType);
        }
    }
}