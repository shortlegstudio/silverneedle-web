// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    
    public interface IFeatGateway
    {
        IEnumerable<Feat> All();

        Feat GetByName(string name);
    }
}