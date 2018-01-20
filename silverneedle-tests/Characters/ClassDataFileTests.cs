// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters
{
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    public class ClassDataFileTests : RequiresDataFiles
    {
        [Fact]
        public void LoadAndAttemptToAddAllClasses()
        {
            var classes = GatewayProvider.All<Class>();

            foreach(var c in classes)
            {
                var bob = CharacterTestTemplates.AverageBob().WithSkills();
                bob.SetClass(c);
            }
        }
    }
}