// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Domains
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Characters.Domains;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    
    public class EarthTests : DomainTestBase<Earth>
    {
        public EarthTests()
        {
            InitializeDomain("earth");
        }

        [Fact]
        public void AddFirstLevelGrantsAcidDartAttack()
        {
            var acidDart = character.Offense.Attacks().OfType<AcidDart>().First();
            Assert.Equal(acidDart.UsesPerDay, 6);
            Assert.Equal(acidDart.Damage.ToString(), "1d6+1");
            Assert.Equal(acidDart.ToString(), "Acid Dart 30' (1d6+1 acid)");
        }

        [Fact]
        public void AddsAcidDamageResistance()
        {
            // Level Up Notification
            character.SetLevel(6);
            domain.LeveledUp(character.Components);

            var acid = character.Defense.DamageResistance.First();
            Assert.Equal(acid.DamageType, "acid");
            Assert.Equal(acid.Amount, 10);

            character.SetLevel(12);
            domain.LeveledUp(character.Components);
            Assert.Equal(acid.Amount, 20);

            //At level 20 remove DR and add immunity
            character.SetLevel(20);
            domain.LeveledUp(character.Components);

            Assert.Equal(acid.Amount, 0);
            var immune = character.Defense.Immunities.First();
            Assert.Equal(immune.Condition, "acid");

        }
    }
}