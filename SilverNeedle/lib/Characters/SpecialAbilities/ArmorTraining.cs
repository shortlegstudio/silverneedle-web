namespace SilverNeedle.Characters.SpecialAbilities
{
    using System.Collections.Generic;
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

        public void Initialize(ComponentBag components)
        {
            components.ApplyStatModifier(MaxDexBonusModifier);
            components.ApplyStatModifier(ArmorCheckBonusModifier);
        }
    }    
}