// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using System.Collections.Generic;
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class ElementalBloodlineTests : RequiresDataFiles
    {
        [Fact]
        public void ChoosesAnElementalTypeWhenAdded()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer().WithSkills();
            var elemental = ElementalBloodline.Create("elemental", 
                new string[] { "perception" }, 
                new Dictionary<int, string>(),
                new Dictionary<int, string>(),
                new string[] { });
            sorcerer.Add(elemental);
            var elementalType = sorcerer.Get<ElementalType>();
            Assert.NotNull(elementalType);
            Assert.Equal(string.Format("Elemental Bloodline ({0})", elementalType.Name), elemental.DisplayString());
        }
    }

}