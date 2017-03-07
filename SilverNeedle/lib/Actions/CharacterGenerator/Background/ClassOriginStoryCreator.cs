// //-----------------------------------------------------------------------
// // <copyright file="ClassOriginStoryCreator.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using SilverNeedle.Characters;
using SilverNeedle.Characters.Background;

namespace SilverNeedle.Actions.CharacterGenerator.Background
{
    public class ClassOriginStoryCreator : ICharacterBuildStep
    {
        IClassOriginGateway classOrigins;

        public ClassOriginStoryCreator(IClassOriginGateway classOrigins)
        {
            this.classOrigins = classOrigins;
        }

        public ClassOriginStoryCreator()
        {
            this.classOrigins = GatewayProvider.Instance().ClassOrigins;
        }

        public ClassOrigin CreateStory(string cls)
        {
            return classOrigins.ChooseOne(cls);
        }

        public void ProcessFirstLevel(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            character.History.ClassOriginStory = CreateStory(character.Class.Name);
        }

        public void ProcessLevelUp(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            throw new NotImplementedException();
        }
    }
}

