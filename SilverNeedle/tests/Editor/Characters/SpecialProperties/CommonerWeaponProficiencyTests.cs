// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialProperties
{
    using NUnit.Framework;
    using SilverNeedle.Characters.SpecialProperties;

    [TestFixture]
    public class CommonerWeaponProficiencyTests
    {
        [Test]
        public void ChooseASimpleWeaponNameEachTime()
        {
            var prop = new CommonerWeaponProficiency();
            var wpn = prop.GetString();

            Assert.That(wpn, Is.Not.Null);
            Assert.That(wpn.Length, Is.GreaterThan(0));
        }
    }
}