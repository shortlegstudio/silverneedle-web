// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

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

