// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator
{
    using Moq;
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;
    
    [TestFixture]
    public class DesignerExecuterStepTests
    {
        [Test]
        public void ExecutesTheDesignerRequested()
        {
            var character = new CharacterSheet();
            var build = new CharacterBuildStrategy();
            var mockDesigner = new Mock<CharacterDesigner>();
            
            // Set up a character designer
            mockDesigner.Setup(x => x.Process(character, build));
            mockDesigner.Setup(x => x.Matches("hello")).Returns(true);
            var gateway = new EntityGateway<CharacterDesigner>(new CharacterDesigner[] { mockDesigner.Object});

            var executer = new DesignerExecuterStep("hello", gateway);
            executer.Process(character, build);
            mockDesigner.VerifyAll();
        }
    }
}