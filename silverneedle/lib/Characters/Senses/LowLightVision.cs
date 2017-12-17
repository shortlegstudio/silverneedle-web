// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Senses
{
    public class LowLightVision : ISense, INameByType
    {
        public string DisplayString()
        {
            return this.Name();
        }
    }
}