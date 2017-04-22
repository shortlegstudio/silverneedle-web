namespace SilverNeedle.Characters.SpecialAbilities
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;

    /// <summary>
    /// Provides the ability to move at full speed with heavier armors and 
    /// reduces the maximum dex penalty of armor
    /// </summary>
    public class ArmorTraining : LevelAbility, IComponent
    {
        public ArmorTraining()
        {
            MaxDexBonusModifier = new DelegateStatModifier(
                StatNames.MaxDexterityBonus, 
                "Ability", 
                "Armor Training",
                () => {return this.Level; }
            );

            ArmorCheckBonusModifier = new DelegateStatModifier(
                StatNames.ArmorCheckPenalty,
                "Ability",
                "Armor Training",
                () => { return this.Level; }
            );
        }

        public IStatModifier MaxDexBonusModifier { get; private set; }

        public IStatModifier ArmorCheckBonusModifier { get; private set; }

        public IStatModifier ArmorMovementBonusModifier { get; private set; }

        public void Initialize(ComponentBag components)
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
            public ArmorMovementModifier(ComponentBag components) : base(StatNames.ArmorMovementPenalty, "Armor Training", "Armor Training")
            {
                this.inventory = components.Get<Inventory>();
                this.training = components.Get<ArmorTraining>();
                this.movementStats = components.Get<MovementStats>();
                this.Calculation = CounterArmorMovePenalty;
            }

            public float CounterArmorMovePenalty()
            {
                IEnumerable<Armor> armorToNegate;
                if(training.Level == 1)
                {
                    //Counter movement penalty from medium armor
                    armorToNegate = inventory.Equipped<Armor>().Where(x => x.ArmorType == ArmorType.Medium);
                }
                else
                {
                    //Counter all armor movement penalty
                    armorToNegate = inventory.Equipped<Armor>();
                }
                if(movementStats.BaseMovement.BaseValue == 30)
                {
                    return -armorToNegate.Sum(x => x.MovementSpeedPenalty30);
                }
                return -armorToNegate.Sum(x => x.MovementSpeedPenalty20);
            }
        }
    }    
}