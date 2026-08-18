using Rx.Http.Serializers;
using System.IO;
using Xunit;

namespace Rx.Http.Tests
{
    public class XmlSerializerTests
    {
        private readonly XmlSerializer serializer = new XmlSerializer();

        [Fact]
        public void TestSerializeProducesXmlWithPropertyValues()
        {
            var model = new SerializerTestModel { Id = 1, Name = "Foo" };

            using var stream = (MemoryStream)serializer.Serialize(model);
            var xml = System.Text.Encoding.UTF8.GetString(stream.ToArray());

            Assert.Contains("<Id>1</Id>", xml);
            Assert.Contains("<Name>Foo</Name>", xml);
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
