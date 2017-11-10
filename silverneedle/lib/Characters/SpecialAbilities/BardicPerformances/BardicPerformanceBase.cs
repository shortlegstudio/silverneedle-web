// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities.BardicPerformances
{
    public abstract class BardicPerformanceBase : IBardicPerformance
    {
        public virtual string Description
        {
            get { return this.GetType().Name.Titlize(); }
        }
    }
}