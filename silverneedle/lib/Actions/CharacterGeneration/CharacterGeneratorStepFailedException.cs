// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration
{
    public class CharacterGeneratorStepFailedException : System.Exception
    {
        public ICharacterDesignStep FailedStep { get; private set; }
        public CharacterGeneratorStepFailedException(ICharacterDesignStep step, string message) : base(message) 
        { 
            this.FailedStep = step;
        }
    }
}