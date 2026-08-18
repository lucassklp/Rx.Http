using Rx.Http;
using Xunit;

namespace Rx.Http.Tests
{
    public class ListDictionaryTests
    {
        [Fact]
        public void TestAppendCreatesNewKeyWithSingleValue()
        {
            var dictionary = new ListDictionary<string, string>();

            dictionary.Append("Foo", "Bar");

            Assert.True(dictionary.ContainsKey("Foo"));
            Assert.Single(dictionary["Foo"]);
            Assert.Equal("Bar", dictionary["Foo"][0]);
        }

        [Fact]
        public void TestAppendAddsToExistingKey()
        {
            var dictionary = new ListDictionary<string, string>();

            dictionary.Append("Foo", "Bar");
            dictionary.Append("Foo", "Baz");

            Assert.Single(dictionary);
            Assert.Equal(new[] { "Bar", "Baz" }, dictionary["Foo"]);
        }

        [Fact]
        public void TestAppendReturnsSameInstanceForChaining()
        {
            var dictionary = new ListDictionary<string, string>();

            var result = dictionary.Append("Foo", "Bar");

            Assert.Same(dictionary, result);
        }

        [Fact]
        public void TestAppendKeepsKeysIndependent()
        {
            var dictionary = new ListDictionary<string, string>();

            dictionary.Append("Foo", "Bar");
            dictionary.Append("User", "John");

            Assert.Equal(2, dictionary.Count);
            Assert.Equal(new[] { "Bar" }, dictionary["Foo"]);
            Assert.Equal(new[] { "John" }, dictionary["User"]);
        }
    }
}
