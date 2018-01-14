// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Dice;
    using SilverNeedle.Utility;
    using SilverNeedle.Serialization;

    public class ChannelEnergy : IAbility, IComponent, IAttack, INameByType, IUsesPerDay
    {

        public ChannelEnergy(IObjectStore configuration)
        {
            SaveDC = new BasicStat(configuration.GetObject("save-dc-stat"));
            DamageDice = new DiceStatistic(configuration.GetObject("dice-stat"));
        }
        public const string POSITIVE_ENERGY = "positive";
        public const string NEGATIVE_ENERGY = "negative";

        public string EnergyType { get; private set; }

        public string Name { get { return this.Name(); } }

        public IDiceStatistic DamageDice { get; private set; }
        public Cup Damage { get { return DamageDice.Dice; } }
        
        public IValueStatistic SaveDC { get; private set; }

        public AttackTypes AttackType { get { return AttackTypes.Special; } }

        public int UsesPerDay => throw new System.NotImplementedException();

        public string DisplayString()
        {
            return "Channel {0} Energy ({1}, DC {2})".Formatted(EnergyType, Damage, SaveDC.TotalValue);
        }

        public void Initialize(ComponentContainer components)
        {
            var alignment = components.Get<CharacterAlignment>();
            if(ChoosesPositiveEnergy(alignment))
            {
                EnergyType = POSITIVE_ENERGY;
            }
            else 
            {
                EnergyType = NEGATIVE_ENERGY;
            }

            components.Add(this.SaveDC);
            components.Add(this.DamageDice);
        }

        private bool ChoosesPositiveEnergy(CharacterAlignment alignment)
        {
            if(alignment.IsGood())
                return true;

            if(alignment.IsEvil())
                return false;
            
            return Randomly.TrueFalse();

        }

        private ChannelEnergy(string dice)
        {
            this.SaveDC = new BasicStat("Channel Energy Save DC");
            this.DamageDice = new DiceStatistic("Channel Energy Dice", dice);
        }

        public static ChannelEnergy CreateForTests(string dice)
        {
            var channel = new ChannelEnergy(dice);
            return channel;
        }

    }
}