// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using System.Linq;
    using SilverNeedle.Characters.SpecialAbilities;

    public class TongueOfSunAndMoonTests 
    {
        [Fact]
        public void AddsToTheLanguageList()
        {
            var bob = CharacterTestTemplates.AverageBob();
            bob.Add(new TongueOfSunAndMoon());
            Assert.Contains("Tongue of Sun and Moon", bob.Languages.Select(x => x.Name));
        }
    }
}