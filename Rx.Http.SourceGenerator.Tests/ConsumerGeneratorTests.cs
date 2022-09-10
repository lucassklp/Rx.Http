using Xunit;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Reactive.Linq;
using Rx.Http.SourceGenerator.OpenApi;
using System.IO;
using Newtonsoft.Json;

namespace Rx.Http.SourceGenerator.Tests
{
    public class ConsumerGeneratorTests
    {

        [Fact]
        public void GenerateEndpoints()
        {
            //var openApi = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "swagger.json")));
            var openApi = JsonConvert.DeserializeObject<JObject>(File.ReadAllText("swagger.json"));
            var generator = new OpenApiInterpreter(openApi!);
            var metadata = generator.GetMetadata();


            // Gera a string da classe



            Assert.NotNull(metadata);
        }
    }
}