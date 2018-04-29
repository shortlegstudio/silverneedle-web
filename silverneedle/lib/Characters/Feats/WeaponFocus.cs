// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Feats
{
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Serialization;
    using SilverNeedle.Equipment;
    using SilverNeedle.Utility;

    public class WeaponFocus : Feat
    {
        private EntityGateway<Weapon> weaponsGateway;
        private EntityGateway<Weapon> GetWeapons()
        {
            if(weaponsGateway == null)
                weaponsGateway = GatewayProvider.Get<Weapon>();

            return weaponsGateway;
        }

        public WeaponFocus(IObjectStore configure) : base(configure) { }
        public WeaponFocus(WeaponFocus copy) : base(copy) { }

        public string WeaponName { get; private set; }
        public WeaponAttackModifier AttackModifier { get; private set; }

        public override Feat Copy()
        {
            return new WeaponFocus(this);
        }

        public override bool IsQualified(CharacterSheet character)
        {
            var result = base.IsQualified(character);
            var possibileWeapons = GetWeapons().FindByProficient(character.Offense.WeaponProficiencies);
            return result && !character.Contains<WeaponFocus>() && possibileWeapons.NotEmpty();
        }

        public override void Initialize(ComponentContainer components)
        {
            base.Initialize(components);
            var offense = components.Get<OffenseStats>();
            var possibileWeapons = GetWeapons().FindByProficient(offense.WeaponProficiencies).CreateFlatTable();
            WeaponName = possibileWeapons.ChooseRandomly().Name;
            this.Name = "{0} ({1})".Formatted(this.Name, this.WeaponName);
            this.AttackModifier = new WeaponAttackModifier(
                1,
                x => { return x.Name == WeaponName; }
            );
            components.Add(this.AttackModifier);
        }

        public static WeaponFocus CreateForTesting(EntityGateway<Weapon> weapons)
        {
            var configure = new MemoryStore();
            configure.SetValue("name", "Weapon Focus");
            var weaponFocus = new WeaponFocus(configure);
            weaponFocus.weaponsGateway = weapons;
            return weaponFocus;
        }
    }
}