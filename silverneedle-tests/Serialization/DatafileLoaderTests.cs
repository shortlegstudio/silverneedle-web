// Copyright (c) 2016 Trevor Redfern
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

namespace Tests.Serialization
{
    using Xunit;
    using System.Linq;
    using SilverNeedle.Serialization;

    public class DatafileLoaderTests : RequiresDataFiles
    {
        [Fact]
        public void CanFindFilesOfSpecificObjectType()
        {
            // TODO: It's a bad test because it's dependent on data files.
            // On the otherhand it also ensures some data files
            var datafileLoader = new DatafileLoader();
            var files = datafileLoader.GetDataFiles<SilverNeedle.Characters.PersonalityType>();
            Assert.True(files.Count() > 0);
        }

        [Fact]
        public void IfTypeIsNotFoundThenThroughSaneException()
        {
            var datafileLoader = new DatafileLoader();
            Assert.Throws<MissingDataFileException>(() => datafileLoader.GetDataFiles<ShouldNeverExistInADataFile>());
        }

        private class ShouldNeverExistInADataFile : IGatewayObject
        {
            public bool Matches(string name)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}