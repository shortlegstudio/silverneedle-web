// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class DemonicMightTests
    {
        [Fact]
        public void AddsDRAndImmunities()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            var might = new DemonicMight();
            sorcerer.Add(might);

            AssertExtensions.Contains("acid", sorcerer.Defense.DamageResistance.Select(x => x.DamageType));
            AssertExtensions.Contains("cold", sorcerer.Defense.DamageResistance.Select(x => x.DamageType));
            AssertExtensions.Contains("fire", sorcerer.Defense.DamageResistance.Select(x => x.DamageType));
            AssertExtensions.Contains("electricity", sorcerer.Defense.Immunities.Select(x => x.Condition));
            AssertExtensions.Contains("poison", sorcerer.Defense.Immunities.Select(x => x.Condition));
        }
    }

}