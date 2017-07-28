// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Bestiary;

    
    public class SummonFamiliarTests
    {
        [Fact]
        public void ChooseAFamiliarFromList()
        {
            var familiar = new Familiar("Bat");
            var subject = new SummonFamiliar(familiar);
            Assert.IsType<Familiar>(subject.Familiar);
        }
    }
}