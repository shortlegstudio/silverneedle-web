// //-----------------------------------------------------------------------
// // <copyright file="ClassOriginStoryCreator.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace SilverNeedle.Actions.CharacterGenerator.Background
{
    using System;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Background;
    using SilverNeedle.Utility;

    public class ClassOriginStoryCreator : ICharacterDesignStep
    {
        EntityGateway<ClassOriginGroup> classOrigins;

        public ClassOriginStoryCreator(EntityGateway<ClassOriginGroup> classOrigins)
        {
            this.classOrigins = classOrigins;
        }

        public ClassOriginStoryCreator()
        {
            this.classOrigins = GatewayProvider.Get<ClassOriginGroup>();
        }

        public ClassOrigin CreateStory(string cls)
        {
            var origins = classOrigins.Find(cls);
            if(origins == null)
                return new ClassOrigin();
            return origins.Origins.ChooseRandomly();            
        }

        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            character.History.ClassOriginStory = CreateStory(character.Class.Name);
        }
    }
}

