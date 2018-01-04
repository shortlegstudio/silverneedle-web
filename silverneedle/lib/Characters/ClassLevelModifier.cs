// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;
    public class ClassLevelModifier : IStatModifier, IComponent
    {
        private ClassLevel classLevel;
        public ClassLevelModifier(IObjectStore configuration)
        {
            configuration.Deserialize(this);
        }

        [ObjectStore("rate")]
        public int LevelRate { get; private set; }

        [ObjectStore("class")]
        public string ClassName { get; private set; }

        public float Modifier 
        { 
            get { return (classLevel.Level / LevelRate).AtLeast(Minimum); } 
        }

        [ObjectStoreOptional("reason")]
        public string Reason { get; private set; }

        [ObjectStore("modifier-type")]
        public string ModifierType { get; private set; }

        [ObjectStore("name")]
        public string StatisticName { get; private set; }

        [ObjectStoreOptional("condition")]
        public string Condition { get; private set; }

        [ObjectStoreOptional("minimum")]
        public int Minimum { get; private set; }
        [ObjectStoreOptional("stat-type")]
        public string StatisticType { get; private set; }

        public void Initialize(ComponentContainer components)
        {
            this.classLevel = components.Get<ClassLevel>();
        }
    }
}