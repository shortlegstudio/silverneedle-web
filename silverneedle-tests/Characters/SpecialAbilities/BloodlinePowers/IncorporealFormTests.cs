// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.SpecialAbilities.BloodlinePowers
{
    using Xunit;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;

    public class IncorporealFormTests
    {
        [Fact]
        public void RoundsPerDayBasedOnSorcererLevel()
        {
            var sorcerer = CharacterTestTemplates.Sorcerer();
            var incForm = new IncorporealForm();
            sorcerer.Add(incForm);


            Assert.Equal(1, incForm.RoundsPerDay);
            sorcerer.SetLevel(15);
            Assert.Equal(15, incForm.RoundsPerDay);
        }
    }
}