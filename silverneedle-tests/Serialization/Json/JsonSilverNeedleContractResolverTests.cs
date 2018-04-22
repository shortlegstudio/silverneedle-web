// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Serialization
{
    using Xunit;
    using SilverNeedle.Serialization;
    using Newtonsoft.Json;

    public class JsonSilverNeedleContractResolverTests
    {
        [Fact]
        public void ExcludesMethodsFromResolving()
        {
            var resolver = new JsonSilverNeedleContractResolver();
            var obj = new SomeTestObject();
            obj.AFunction = () => { return 3; };
            obj.BFunction = (x) => { return false; };
            var json = JsonConvert.SerializeObject(obj, new JsonSerializerSettings { ContractResolver = resolver } );
            Assert.NotEmpty(json);
        }


        public class SomeTestObject
        {
            public string Name { get { return "Foobar"; } }
            public System.Func<int> AFunction { get; set; }
            public System.Func<int, bool> BFunction { get; set; }
        }
    }
}