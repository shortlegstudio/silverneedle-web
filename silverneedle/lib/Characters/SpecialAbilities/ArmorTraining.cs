// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    /// <summary>
    /// Provides the ability to move at full speed with heavier armors and 
    /// reduces the maximum dex penalty of armor
    /// </summary>
    public class ArmorTraining : CapabilityStatistic, IComponent
    {
        public ComponentContainer Parent { get; set; }
        public ArmorTraining(IObjectStore configuration) : base(configuration)
        {
            MaxDexBonusModifier = new DelegateStatModifier(
                StatNames.MaxDexterityBonus, 
                "Ability", 
                () => {return this.TotalValue; }
            );

            ArmorCheckBonusModifier = new DelegateStatModifier(
                StatNames.ArmorCheckPenalty,
                "Ability",
                () => { return this.TotalValue; }
            );
        }

        public IValueStatModifier MaxDexBonusModifier { get; private set; }

        public IValueStatModifier ArmorCheckBonusModifier { get; private set; }

        public IValueStatModifier ArmorMovementBonusModifier { get; private set; }

        public void Initialize(ComponentContainer components)
        {
            ArmorMovementBonusModifier = new ArmorMovementModifier(components);
            components.ApplyStatModifier(MaxDexBonusModifier);
            components.ApplyStatModifier(ArmorCheckBonusModifier);
            components.ApplyStatModifier(ArmorMovementBonusModifier);
        }


        private class ArmorMovementModifier : DelegateStatModifier
        {
            private Inventory inventory;
            private ArmorTraining training;
            private MovementStats movementStats;
            public ArmorMovementModifier(ComponentContainer components) : base(StatNames.ArmorMovementPenalty, "Armor Training")
            {
                this.inventory = components.Get<Inventory>();
                this.training = components.Get<ArmorTraining>();
                this.movementStats = components.Get<MovementStats>();
                this.Calculation = CounterArmorMovePenalty;
            }

            public float CounterArmorMovePenalty()
            {
                IEnumerable<Armor> armorToNegate;
                if(training.TotalValue == 1)
                {
                    //Counter movement penalty from medium armor
                    armorToNegate = inventory.Equipped<Armor>().Where(x => x.ArmorType == ArmorType.Medium);
                }
                else
                {
                    //Counter all armor movement penalty
                    armorToNegate = inventory.Equipped<Armor>();
                }
                if(movementStats.UseBase30MoveSpeed)
                {
                    return -armorToNegate.Sum(x => x.MovementSpeedPenalty30);
                }
                return -armorToNegate.Sum(x => x.MovementSpeedPenalty20);
            }
        }
    }    
}