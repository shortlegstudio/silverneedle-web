// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    [System.Serializable]
    public class MissingCharacterClassException : System.Exception
    {
        public MissingCharacterClassException() : base("Character must have a class assigned for this operation.")
        {

        }
    
    }
}