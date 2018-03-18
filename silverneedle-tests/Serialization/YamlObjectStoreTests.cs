// Copyright (c) 2017 Trevor Redfern
//
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Serialization
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class YamlObjectStoreTests
    {
        [Fact]
        public void ProvidesAListOfKeysForNode()
        {
            string demo = @"---
node1: value
node2: value
node3: value
node4: value
...";
            IObjectStore y = demo.ParseYaml();
            Assert.Equal(4, y.Keys.Count());
            Assert.Equal("node1", y.Keys.ElementAt(0));
            Assert.Equal("node2", y.Keys.ElementAt(1));
            Assert.Equal("node3", y.Keys.ElementAt(2));
            Assert.Equal("node4", y.Keys.ElementAt(3));
        }

        [Fact]
        public void CanGetBooleanValues()
        {
            string test = "boolean: true";
            IObjectStore n = test.ParseYaml();
            Assert.True(n.GetBool("boolean"));

            string falsies = "boolean: false";
            n = falsies.ParseYaml();
            Assert.False(n.GetBool("boolean"));
        }

        [Fact]
        public void GetListWillReturnStringListIfKeyHasChildren()
        {
            string list = @"---
list:
    - one
    - two
    - three
";
            IObjectStore parsedYaml = list.ParseYaml();
            string[] items = parsedYaml.GetList("list");
            Assert.Equal(new string[] { "one", "two", "three"}, items);
        }

        [Fact]
        public void ReturnsPercentageSignsInListsIfStillThere()
        {
            string list = @"---
list: [""%foo%"", ""%bar%""]";
            var parsed = list.ParseYaml();
            Assert.Equal(new string [] { "%foo%", "%bar%" }, parsed.GetList("list"));
        }

        [Fact]
        public void CanReturnAListOfObjects()
        {
            var yaml = @"
list:
  - name: key
    value: 39
  - name: key2
    value: 38";
            var store = yaml.ParseYaml();
            var list = store.GetObjectList("list");
            Assert.Equal("key", list.First().GetString("name"));
            Assert.Equal(39, list.First().GetInteger("value"));
            Assert.Equal("key2", list.Last().GetString("name"));
            Assert.Equal(38, list.Last().GetInteger("value"));

        }

        [Fact]
        public void CanSetValuesIntoMappingElementForSerialization()
        {
            var yamlStore = new YamlObjectStore();
            yamlStore.SetValue("name", "A Name");
            yamlStore.SetValue("number", 2383);
            yamlStore.SetValue("list-of-values", new string[] { "one", "two", "three" });
            yamlStore.SetValue("boolean", true);
            yamlStore.SetValue("float-val", 283.238f);
            var childObj = new YamlObjectStore();
            childObj.SetValue("name", "childName");
            yamlStore.SetValue("child", childObj);

            var listOfObject = new YamlObjectStore[] { new YamlObjectStore(), new YamlObjectStore() };
            listOfObject[0].SetValue("name", "Test 1");
            listOfObject[1].SetValue("name", "Test 2");
            yamlStore.SetValue("list-of-objects", listOfObject);
            
            Assert.Equal("A Name", yamlStore.GetString("name"));
            Assert.Equal(2383, yamlStore.GetInteger("number"));
            Assert.True(yamlStore.GetBool("boolean"));
            Assert.Equal(283.238f, yamlStore.GetFloat("float-val"));
            Assert.Equal(new string[] { "one", "two", "three" }, yamlStore.GetList("list-of-values"));
            Assert.Equal("childName", yamlStore.GetObject("child").GetString("name"));
            Assert.Equal(2, yamlStore.GetObjectList("list-of-objects").Count());
        }
    }
}