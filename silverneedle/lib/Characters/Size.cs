// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    [ObjectStoreSerializable]
    public class Size : IGatewayObject, IEnsureUniqueComponent, IComponent
    {
        [ObjectStore("name")]
        public CharacterSize Name { get; private set; }

        [ObjectStore("attack-modifier")]
        public int AttackModifier { get; private set; }

        [ObjectStore("defense-modifier")]
        public int DefenseModifier { get; private set; }

        [ObjectStore("combat-maneuver-modifier")]
        public int CombatManeuverModifier { get; private set; }

        [ObjectStore("fly-modifier")]
        public int FlyModifier { get; private set; }

        [ObjectStore("stealth-modifier")]

        public int StealthModifier { get; private set; }

        public bool ReplaceExistingComponent { get { return true; } }

        public ComponentContainer Parent { get; set; }

        public bool ComponentUniquenessComparison(object obj)
        {
            return obj is Size;
        }

        public void Initialize(ComponentContainer components)
        {
            components.FindStat<BasicStat>(StatNames.SizeAttackModifier).SetValue(this.AttackModifier);
            components.FindStat<BasicStat>(StatNames.SizeDefenseModifier).SetValue(this.DefenseModifier);
            components.FindStat<BasicStat>(StatNames.SizeCombatManeuverModifier).SetValue(this.CombatManeuverModifier);
            components.FindStat<BasicStat>(StatNames.SizeFlyModifier).SetValue(this.FlyModifier);
            components.FindStat<BasicStat>(StatNames.SizeStealthModifier).SetValue(this.StealthModifier);
        }

        public bool Matches(string name)
        {
            return Name.ToString().EqualsIgnoreCase(name);
        }
    }
}