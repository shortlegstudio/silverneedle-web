// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Prerequisites
{
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Prerequisites;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    [TestFixture]
    public class SpecialAbilityPrerequisiteTests {
        [Test]
        public void SpecialAbilityPrerequisite() 
        {
            var special = new SpecialAbilityPrerequisite("Darkvision");
            var c = new CharacterSheet();
            Assert.That(special.IsQualified(c), Is.False);
            var darkvision = new MemoryStore();
            darkvision.SetValue("name", "Darkvision");
            c.Add(new Trait(darkvision));
            Assert.That(special.IsQualified(c), Is.True);
        }
    }
}