// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;
    public class CasterLevelModifier : IValueStatModifier, IComponent
    {
        private ISpellCasting casting;
        public CasterLevelModifier(IObjectStore configuration)
        {
            configuration.Deserialize(this);
        }

        [ObjectStore("rate")]
        public int LevelRate { get; private set; }

        [ObjectStore("caster-type")]
        public Spells.SpellType CasterType { get; private set; }

        public float Modifier 
        { 
            get { return ((casting.CasterLevel - StartLevel) / LevelRate).AtLeast(Minimum); } 
        }

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

        [ObjectStoreOptional("start-level")]
        public int StartLevel { get; private set; }

        public void Initialize(ComponentContainer components)
        {
            this.casting = components.Get<ISpellCasting>();
        }
    }
}