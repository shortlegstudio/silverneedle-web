// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Equipment
{
    public class ArmorDecorator : IArmor
    {
        private IArmor baseArmor;

        public string Name => string.Format("{0} {1} {2}", this.NamePreface, baseArmor.Name, this.NameSuffix).Trim();

        public int ArmorClass => baseArmor.ArmorClass + this.ArmorClassModifier;

        public float Weight => baseArmor.Weight + this.WeightModifier;

        public int MaximumDexterityBonus => baseArmor.MaximumDexterityBonus + this.MaximumDexterityBonusModifier;

        public int ArmorCheckPenalty => baseArmor.ArmorCheckPenalty + this.ArmorCheckPenaltyModifier;

        public int ArcaneSpellFailureChance => baseArmor.ArcaneSpellFailureChance + this.ArcaneSpellFailureChanceModifier;

        public ArmorType ArmorType => baseArmor.ArmorType;

        public int Value => baseArmor.Value + AdditionalValue;

        public bool GroupSimilar => baseArmor.GroupSimilar;

        public int MovementSpeedPenalty20 => baseArmor.MovementSpeedPenalty20;
        public int MovementSpeedPenalty30 => baseArmor.MovementSpeedPenalty30;

        public ArmorDecorator(IArmor baseArmor)
        {
            this.baseArmor = baseArmor;
        }

        protected int AdditionalValue { get; set; }
        protected string NamePreface { get; set; }
        protected string NameSuffix { get; set; }
        protected int ArmorClassModifier { get; set; }
        protected float WeightModifier { get; set; }
        protected int MaximumDexterityBonusModifier { get; set; }
        protected int ArmorCheckPenaltyModifier { get; set; }
        protected int ArcaneSpellFailureChanceModifier { get; set; }
    }
}