using Newtonsoft.Json.Linq;
using Rx.Http.SourceGenerator.Models;

namespace Rx.Http.SourceGenerator
{
    public class ConsumerGenerator
    {
        private string @namespace;
        public ConsumerGenerator(string @namespace)
        {
            this.@namespace = @namespace;
        }

        public string GenerateClass()
        {
            var result = "using Rx.Http;";
            result += $"\n namespace {@namespace} \n{{";





            result += "\n}";
            return result;
        }
    }
}
