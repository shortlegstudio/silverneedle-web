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

    public class WeaponFocus : Feat, IComponent
    {
        private static EntityGateway<Weapon> weapons;
        private static EntityGateway<Weapon> GetWeapons()
        {
            if(weapons == null)
                weapons = GatewayProvider.Get<Weapon>();

            return weapons;
        }

        public WeaponFocus(EntityGateway<Weapon> weaponGateway) : this() 
        {
            weapons = weaponGateway;
        }
        public WeaponFocus() : base() 
        {
            this.Name = "Weapon Focus";
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

        public void Initialize(ComponentContainer components)
        {
            var offense = components.Get<OffenseStats>();
            var possibileWeapons = GetWeapons().FindByProficient(offense.WeaponProficiencies).CreateFlatTable();
            WeaponName = possibileWeapons.ChooseRandomly().Name;
            this.Name = "{0} ({1})".Formatted(this.Name, this.WeaponName);
            this.AttackModifier = new WeaponAttackModifier(
                this.Name,
                1,
                x => { return x.Name == WeaponName; }
            );
            offense.AddWeaponModifier(this.AttackModifier);
            

        }
    }
}