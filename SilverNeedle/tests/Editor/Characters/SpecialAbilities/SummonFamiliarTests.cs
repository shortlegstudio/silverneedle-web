// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using NUnit.Framework;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Beastiary;

    [TestFixture]
    public class SummonFamiliarTests
    {
        [Test]
        public void ChooseAFamiliarFromList()
        {
            var familiar = new Familiar("Bat");
            var subject = new SummonFamiliar(familiar);
            Assert.That(subject.Familiar, Is.TypeOf(typeof(SilverNeedle.Beastiary.Familiar)));
        }
    }
}