// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Utility
{
    using Xunit;
    using SilverNeedle.Utility;

    public class ReflectorTests
    {
        [Fact]
        public void CanFindTypesUsingStringFromOtherAssemblies()
        {
            var type = Reflector.FindType("Tests.Utility.ReflectorTests");
            Assert.Equal(typeof(ReflectorTests), type);
        }

        [Fact]
        public void FindAllTypesThatImplementInterface()
        {
            var types = Reflector.FindAllTypesThatImplement<IDummy>();
            Assert.NotStrictEqual(new System.Type[] { typeof(DummyOne), typeof(DummyTwo)}, types);
        }

        [Fact]
        public void ThrowExceptionIfTypeNotFound()
        {
            Assert.Throws(typeof(TypeNotFoundException), () => "SomeRandomTypeThatCantExist".Instantiate<object>()); 
        }

        [Fact]
        public void FindsTypesByStringInAnyAssembly()
        {
            var name = typeof(DummyOne).FullName;
            var type = Reflector.FindType(name);
            Assert.Equal(typeof(DummyOne), type); 
        }

        [Fact]
        public void ChecksWhetherClassImplementsAParticularClassOrInterface()
        {
            var test = new DummyTwo();
            Assert.True(test.Implements(typeof(DummyTwo)));
            Assert.True(test.Implements(typeof(IDummy)));
        }

        public interface IDummy { }
        public class DummyOne : IDummy { }
        public class DummyTwo : IDummy { }
    }
}