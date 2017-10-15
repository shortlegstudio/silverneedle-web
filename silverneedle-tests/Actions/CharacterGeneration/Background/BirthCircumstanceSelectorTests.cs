// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.Background
{
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.Background;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Background;
    using SilverNeedle.Serialization;

    public class BirthCircumstanceSelectorTests
    {
        [Fact]
        public void ChoosesFromListOfOptionsAndAssignsToHistory()
        {
            var circumstance = new BirthCircumstance("Nothing exciting", 10);
            var character = CharacterTestTemplates.AverageBob();
            var subject = new BirthCircumstanceSelector(EntityGateway<BirthCircumstance>.LoadWithSingleItem(circumstance));
            subject.ExecuteStep(character, new CharacterStrategy());
            var history = character.Get<History>();
            Assert.Equal(circumstance, history.BirthCircumstance);

        }

    }
}