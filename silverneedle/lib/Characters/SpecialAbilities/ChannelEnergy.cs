// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Dice;
    using SilverNeedle.Utility;
    public class ChannelEnergy : IAbility, IComponent
    {
        public const string POSITIVE_ENERGY = "positive";
        public const string NEGATIVE_ENERGY = "negative";
        public ChannelEnergyAttack ChannelAttack { get; private set; }

        public string EnergyType { get; private set; }
        public string DisplayString()
        {
            return "Channel Energy ({0})".Formatted(EnergyType);
        }

        public void Initialize(ComponentContainer components)
        {
            this.ChannelAttack = new ChannelEnergyAttack(components);
            var alignment = components.Get<CharacterAlignment>();
            if(ChoosesPositiveEnergy(alignment))
            {
                EnergyType = POSITIVE_ENERGY;
            }
            else 
            {
                EnergyType = NEGATIVE_ENERGY;
            }
            
            components.Add(ChannelAttack);
        }

        private bool ChoosesPositiveEnergy(CharacterAlignment alignment)
        {
            if(alignment.IsGood())
                return true;

            if(alignment.IsEvil())
                return false;
            
            return Randomly.TrueFalse();

        }

    }
}