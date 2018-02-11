// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT



namespace SilverNeedle.Characters.Senses
{
    using SilverNeedle.Utility;
    public class Scent : ISense, INameByType
    {
        public string DisplayString()
        {
            return this.Name();
        }
    }
}