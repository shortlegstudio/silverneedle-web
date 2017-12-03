// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Magic
{
    using System.Collections.Generic;
    using SilverNeedle.Utility;
    using SilverNeedle.Serialization;

    public interface IArcaneSchool : IComponent, IImprovesWithLevels, IGatewayObject
    {
        string Name { get; }
        IEnumerable<IObjectStore> GetAbilities();

    }
}