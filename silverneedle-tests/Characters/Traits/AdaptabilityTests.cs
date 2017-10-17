// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Traits
{
    using Xunit;
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Traits;

    public class AdaptabilityTests
    {
        [Fact]
        public void AddingAdaptabilityTraitAddsSkillFocusFeatToken()
        {
            var bob = CharacterTestTemplates.AverageBob();
            bob.Add(new Adaptability());

            var skillFocus = bob.GetAll<FeatToken>().First(x => x.Tags.Contains("Skill Focus"));
            Assert.NotNull(skillFocus);
            
        }
    }
}