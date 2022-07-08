using Xunit;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Reactive.Linq;
using Rx.Http.SourceGenerator.OpenApi;

namespace Rx.Http.SourceGenerator.Tests
{
    public class ConsumerGeneratorTests
    {

        [Fact]
        public void GenerateEndpoints()
        {
            RxHttpClient client = RxHttpClient.Create();
            var openApi = client.Get<JObject>("https://petstore.swagger.io/v2/swagger.json").Wait();
            var generator = new OpenApiInterpreter(openApi!);
            var metadata = generator.GetMetadata();


            // Gera a string da classe



            Assert.NotNull(metadata);
        }
    }
}