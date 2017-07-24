// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Equipment
{
    public class ArmorDecorator : IArmor
    {
        private IArmor baseArmor;

        public virtual string Name => baseArmor.Name;

        public virtual int ArmorClass => baseArmor.ArmorClass;

        public virtual float Weight => baseArmor.Weight;

        public virtual int MaximumDexterityBonus => baseArmor.MaximumDexterityBonus;

        public virtual int ArmorCheckPenalty => baseArmor.ArmorCheckPenalty;

        public virtual int ArcaneSpellFailureChance => baseArmor.ArcaneSpellFailureChance;

        public virtual ArmorType ArmorType => baseArmor.ArmorType;

        public virtual int Value => baseArmor.Value;

        public virtual bool GroupSimilar => baseArmor.GroupSimilar;

        public virtual int MovementSpeedPenalty20 => baseArmor.MovementSpeedPenalty20;
        public virtual int MovementSpeedPenalty30 => baseArmor.MovementSpeedPenalty30;

        public ArmorDecorator(IArmor baseArmor)
        {
            this.baseArmor = baseArmor;
        }
    }
}