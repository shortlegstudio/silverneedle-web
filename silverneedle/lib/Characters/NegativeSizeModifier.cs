// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class NegativeSizeModifier : IValueStatModifier, IComponent
    {
        public ComponentContainer Parent { get; set; }
        public NegativeSizeModifier(IObjectStore configuration)
        {
            configuration.Deserialize(this);
        }
        public float Modifier { get { return -(float)_sizeStats.Size; } }

        [ObjectStore("modifier-type")]
        public string ModifierType { get; private set; }

        [ObjectStoreOptional("condition")]
        public string Condition { get; private set; }

        [ObjectStore("name")]
        public string StatisticName { get; private set; }

        [ObjectStoreOptional("statistic-type")]
        public string StatisticType { get; private set; }

        private SizeStats _sizeStats;

        public void Initialize(ComponentContainer components)
        {
            _sizeStats = components.Get<SizeStats>();
        }
    }
}