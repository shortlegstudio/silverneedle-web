// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using System.Collections.Generic;
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities;

    public class DraconicBloodlineTests : RequiresDataFiles
    {
        [Fact]
        public void ChoosesADragonTypeWhenAdded()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            var draconic = DraconicBloodline.Create("draconic", 
                new string[] { "perception" }, 
                new Dictionary<int, string>(),
                new Dictionary<int, string>(),
                new string[] { });
            sorcerer.Add(draconic);
            Assert.NotNull(draconic.DragonType);
            Assert.Equal(string.Format("Draconic Bloodline ({0})", draconic.DragonType.Name), draconic.DisplayString());
        }
    }

}