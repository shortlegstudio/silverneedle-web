// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Core;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class AbjurationResistance : SpecialAbility, IComponent
    {
        private EntityGateway<EnergyType> energyTypes;
        private DamageResistance damageResistance;
        private ClassLevel sourceLevels;
        public AbjurationResistance() : this(GatewayProvider.Get<EnergyType>())
        {

        }

        public AbjurationResistance(EntityGateway<EnergyType> energyTypes)
        {
            this.energyTypes = energyTypes;
        }

        public void Initialize(ComponentContainer components)
        {
            this.sourceLevels = components.Get<ClassLevel>();
            var energyType = energyTypes.ChooseOne();
            damageResistance = new DamageResistance(energyType, CalculateResistance);
            var defense = components.Get<DefenseStats>();
            defense.AddDamageResistance(damageResistance);
        }

        private float CalculateResistance()
        {
            if(sourceLevels.Level >= 20)
                return DamageResistance.IMMUNITY_THRESHOLD;
            
            if(sourceLevels.Level >= 11)
                return 10;

            return 5;
        }
    }
}