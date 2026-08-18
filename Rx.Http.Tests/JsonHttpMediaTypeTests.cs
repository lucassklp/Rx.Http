using Rx.Http.MediaTypes;
using Rx.Http.Serializers;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Rx.Http.Tests
{
    public class JsonHttpMediaTypeTests
    {
        private readonly JsonHttpMediaType mediaType = new JsonHttpMediaType(new NewtonsoftJsonSerializer());

        [Fact]
        public async Task TestSerializeSetsJsonContentType()
        {
            var model = new SerializerTestModel { Id = 1, Name = "Foo" };

            var content = mediaType.Serialize(model);

            Assert.Equal(MediaType.Application.Json, content.Headers.ContentType.MediaType);
            var json = await content.ReadAsStringAsync();
            Assert.Equal(@"{""Id"":1,""Name"":""Foo""}", json);
        }

        [Fact]
        public void TestDeserializeReadsObjectFromStream()
        {
            var json = @"{""Id"":42,""Name"":""Bar""}";
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));

            var result = mediaType.Deserialize<SerializerTestModel>(stream);

            Assert.Equal(42, result.Id);
            Assert.Equal("Bar", result.Name);
        }
    }
}
