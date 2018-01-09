// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Characters.SpecialAbilities;

    public class KiPoolTests
    {
        [Fact]
        public void KiPoolLooksForKiStrikeAbilities()
        {
            var monk = CharacterTestTemplates.MarkyMonk();
            
            var kiPool = new KiPool();
            monk.Add(kiPool);
            monk.Add(new KiStrike("magic"));
            monk.Add(new KiStrike("lawful"));
            Assert.Contains("magic", kiPool.DisplayString());
            Assert.Contains("lawful", kiPool.DisplayString());
        }
    }
}