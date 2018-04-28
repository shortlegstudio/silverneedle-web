// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;
    public class MonkFastMovement : AbilityDisplayAsName, IComponent
    {
        public ComponentContainer Parent { get; set; }
        private DataTable monkAbilities;
        private IValueStatModifier movementModifier;
        private ClassLevel monkLevels;
        public MonkFastMovement()
        {
            this.monkAbilities = Serialization.GatewayProvider.Find<DataTable>("Monk Abilities");
        }
        public MonkFastMovement(DataTable monkAbilities)
        {
            this.monkAbilities = monkAbilities;
        }
        public void Initialize(ComponentContainer components)
        {
            monkLevels = components.Get<ClassLevel>();
            movementModifier = new DelegateStatModifier(StatNames.BaseMovementSpeed, "Bonus", this.Modifier);
            components.ApplyStatModifier(movementModifier);
        }

        private float Modifier()
        {
            return monkAbilities.Get(monkLevels.Level.ToString(), "fast-movement").ToInteger();
        }
    }
}