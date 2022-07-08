using Newtonsoft.Json.Linq;
using Rx.Http.SourceGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rx.Http.SourceGenerator.OpenApi
{
    public class OpenApiInterpreter
    {
        private JObject openApi;
        public OpenApiInterpreter(JObject openApi)
        {
            this.openApi = openApi;
        }

        public List<EndpointMetadata> GetMetadata()
        {
            var paths = openApi["paths"] as JObject;
            var endpoints = new List<EndpointMetadata>();
            foreach (var path in paths!)
            {
                var endpoint = path.Key;
                var methods = path.Value as JObject;
                foreach (var method in methods!)
                {
                    var endpointMetadata = new EndpointMetadata();
                    var jsonMetadata = (method.Value as JObject)!;

                    endpointMetadata.Endpoint = endpoint;
                    endpointMetadata.HttpMethod = method.Key;
                    endpointMetadata.OperationId = jsonMetadata["operationId"]!.Value<string>()!;
                    endpointMetadata.Consumes = jsonMetadata["consumes"]?.First?.Value<string>();
                    endpointMetadata.Produces = jsonMetadata["produces"]?.First?.Value<string>();

                    //Pega o retorno do código 200; Se não tiver, usa o httpresponse normal.
                    //Pode ser um $ref ou uma definição. Se for uma $ref, usar o valor do model. 
                    //Caso contrário, chamar o ModelGenerator
                    endpointMetadata.ReturnEntity = GetReturnType(jsonMetadata?["responses"]?["200"] as JObject);


                    var parameters = (jsonMetadata["parameters"] as JArray)!;

                    foreach (var parameter in parameters)
                    {
                        var pathParameters = new List<EndpointParameter>();

                        pathParameters.Add(new EndpointParameter
                        {
                            Name = parameter.Value<string>("name")!,
                            Type = parameter.Value<string>("in")!
                        });
                    }

                    endpoints.Add(endpointMetadata);
                }
            }
            return endpoints;
        }

        private string GetReturnType(JObject returnType)
        {
            var schema = returnType?["schema"] as JObject;
            var @ref = schema?["$ref"];
            var type = schema?["type"];
            if (schema == null)
            {
                return "RxHttpResponse";
            }
            //else if (type != null && type.Value<string>() == "array" && type?["$ref"] != null)
            //{
            //    var items = schema?["items"];
            //    var typeValue = items["$ref"]?.Value<string>().Replace("#/definitions/", string.Empty);
            //    return $"List<{typeValue}>";
            //}
            else if (@ref != null)
            {
                return @ref.Value<string>().Replace("#/definitions/", string.Empty);
            }
            else return "RxHttpResponse";
        }

    }
}
