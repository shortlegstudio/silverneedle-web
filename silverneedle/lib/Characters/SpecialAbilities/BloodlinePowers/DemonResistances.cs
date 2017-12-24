// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;
    public class DemonResistances : SpecialAbility, IComponent, IImprovesWithLevels, IBloodlinePower
    {
        private ConditionalStatModifier poisonResistance;
        private EnergyResistance damageResistance;
        public void Initialize(ComponentContainer components)
        {
            var level = components.Get<ClassLevel>();
            poisonResistance = new ConditionalStatModifier(new DelegateStatModifier("saves", "bonus", this.Name, () => { return level.Level >= 9 ? 4 : 2; }), "poison");
            damageResistance = new EnergyResistance(5, "electricity");
            var defense = components.Get<DefenseStats>();
            defense.FortitudeSave.AddModifier(poisonResistance);
            defense.ReflexSave.AddModifier(poisonResistance);
            defense.WillSave.AddModifier(poisonResistance);
            defense.AddDamageResistance(damageResistance);
        }

        public void LeveledUp(ComponentContainer components)
        {
            var level = components.Get<ClassLevel>();
            if(level.Level >= 9)
            {
                damageResistance.Amount = 10;
            }
        }
    }
}