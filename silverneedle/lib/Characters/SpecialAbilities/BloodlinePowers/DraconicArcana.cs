// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.SpecialAbilities.BloodlinePowers
{
    using SilverNeedle.Utility;
    public class DraconicArcana : BloodlineArcana, IComponent
    {
        public ComponentContainer Parent { get; set; }
        private DragonType dragonType;

        public override string BonusAbility { get { return "{0} spells deal +1 damage per die".Formatted(dragonType.EnergyType); } }

        public void Initialize(ComponentContainer components)
        {
            var bloodline = components.Get<IDraconicBloodline>();
            this.dragonType = bloodline.DragonType;
        }
    }
}