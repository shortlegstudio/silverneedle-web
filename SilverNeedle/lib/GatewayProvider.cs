// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

namespace SilverNeedle
{
    using SilverNeedle.Characters;
    using SilverNeedle.Equipment;
    using SilverNeedle.Names;
    public class GatewayProvider
    {
        public GatewayProvider()
        {
            SetAllYaml();
        }

        public IArmorGateway Armors { get; set; }
        public IWeaponGateway Weapons { get; set; }
        public IEntityGateway<Skill> Skills { get; set; }

        public IClassGateway Classes { get; set; }

        public IRaceMaturityGateway Maturity { get; set; }

        public IRaceGateway Races { get; set; }

        public ICharacterNamesGateway Names { get; set; }

        public IFeatGateway Feats { get; set; }

        public void SetAllYaml() {
            Armors = new ArmorYamlGateway();
            Weapons = new WeaponYamlGateway();
            Skills = new SkillYamlGateway();
            Classes = new ClassYamlGateway();
            Maturity = new RaceMaturityYamlGateway();
            Races = new RaceYamlGateway();
            Names = new CharacterNamesYamlGateway();
            Feats = new FeatYamlGateway();
        }
    }
}