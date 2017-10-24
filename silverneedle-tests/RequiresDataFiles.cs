﻿// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using System;
namespace Tests
{
	public class RequiresDataFiles
    {
        public RequiresDataFiles()
        {
            SilverNeedle.Configuration.DataPath = "../../../../data";
        }
    }
}
