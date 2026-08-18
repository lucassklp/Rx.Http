using Rx.Http.Serializers;
using System.IO;
using Xunit;

namespace Rx.Http.Tests
{
    public class NewtonsoftJsonSerializerTests
    {
        private readonly NewtonsoftJsonSerializer serializer = new NewtonsoftJsonSerializer();

        [Fact]
        public void TestSerializeProducesJson()
        {
            var model = new SerializerTestModel { Id = 1, Name = "Foo" };

            using var stream = (MemoryStream)serializer.Serialize(model);
            var json = System.Text.Encoding.UTF8.GetString(stream.ToArray());

            Assert.Equal(@"{""Id"":1,""Name"":""Foo""}", json);
        }

        [Fact]
        public void TestDeserializeReadsBackSerializedObject()
        {
            var model = new SerializerTestModel { Id = 42, Name = "Bar" };

            var stream = serializer.Serialize(model);
            var deserialized = serializer.Deserialize<SerializerTestModel>(stream);

            Assert.Equal(model.Id, deserialized.Id);
            Assert.Equal(model.Name, deserialized.Name);
        }
    }
}
