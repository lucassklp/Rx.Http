using Rx.Http.Serializers;
using System.IO;
using Xunit;

namespace Rx.Http.Tests
{
    public class NativeJsonSerializerTests
    {
        private readonly NativeJsonSerializer serializer = new NativeJsonSerializer();

        [Fact]
        public void TestSerializeProducesJson()
        {
            var model = new SerializerTestModel { Id = 1, Name = "Foo" };

            using var stream = (MemoryStream)serializer.Serialize(model);
            var json = System.Text.Encoding.UTF8.GetString(stream.ToArray());

            Assert.Equal(@"{""Id"":1,""Name"":""Foo""}", json);
        }

        [Fact]
        public void TestSerializeReturnsStreamPositionedAtStart()
        {
            var model = new SerializerTestModel { Id = 1, Name = "Foo" };

            var stream = serializer.Serialize(model);

            Assert.Equal(0, stream.Position);
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
