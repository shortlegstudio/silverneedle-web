// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php


using System;
using NUnit.Framework;
using System.Linq;
using SilverNeedle;

namespace Utility
{
    [TestFixture]
    public class DatafileLoaderTests
    {
        [Test]
        public void ProvidesAccessToAListOfFilesThatMatchCriteria()
        {
            var datafileLoader = new DatafileLoader();
            var files = datafileLoader.GetDataFiles("armor");
            Assert.Greater(files.Count(), 0);
        }
    }
}