// //-----------------------------------------------------------------------
// // <copyright file="ClassOriginStoryCreator.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace SilverNeedle.Actions.CharacterGeneration.Background
{
    using System;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Background;
    using SilverNeedle.Serialization;

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
            var origins = classOrigins.FindOrNull(cls);
            if(origins == null)
                return new ClassOrigin();
            return origins.Origins.ChooseRandomly();            
        }

        public void ExecuteStep(CharacterSheet character)
        {
            character.History.ClassOriginStory = CreateStory(character.Class.Name);
        }
    }
}

