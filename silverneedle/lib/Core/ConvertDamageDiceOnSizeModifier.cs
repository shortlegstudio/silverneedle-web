// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle
{
    using SilverNeedle.Characters;
    using SilverNeedle.Dice;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class ConvertDamageDiceOnSizeModifier : IDiceModifier, Utility.IComponent
    {
        public ComponentContainer Parent { get; set; }
        private SizeStats sizeStats;

        [ObjectStore("name")]
        public string StatisticName { get; private set; }

        [ObjectStoreOptional("statistic-type")]
        public string StatisticType { get; private set; }

        public CharacterSize Size { get { return sizeStats.Size; } }

        public void Initialize(ComponentContainer components)
        {
            sizeStats = components.Get<SizeStats>();
        }

        public void ProcessModifier(Cup dice)
        {
            var copy = dice.Copy();
            dice.Dice.Clear();
            dice.AddDice(Equipment.DamageTables.ConvertDamageBySize(copy, Size).Dice);
        }

        public ConvertDamageDiceOnSizeModifier(IObjectStore configuration)
        {
            configuration.Deserialize(this);
        }
    }
}