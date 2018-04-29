// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration
{
    using Moq;
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;
    
    
    public class DesignerExecuterStepTests
    {
        [Fact]
        public void ExecutesTheDesignerRequested()
        {
            var character = CharacterTestTemplates.AverageBob();
            var mockDesigner = new Mock<CharacterDesigner>();
            
            // Set up a character designer
            mockDesigner.Setup(x => x.ExecuteStep(character));
            mockDesigner.Setup(x => x.Matches("hello")).Returns(true);
            var gateway = EntityGateway<CharacterDesigner>.LoadWithSingleItem(mockDesigner.Object);

            var executer = new DesignerExecuterStep("hello", gateway);
            executer.ExecuteStep(character);
            mockDesigner.VerifyAll();
        }
    }
}