using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rx.Http.SourceGenerator.Models
{
    public class EndpointMetadata
    {
        public string Endpoint { get; set; }
        public string HttpMethod { get; set; }
        public string OperationId { get; set; }
        public string Consumes { get; set; }
        public string Produces { get; set; }
        public string ReturnEntity { get; set; }

        public List<EndpointParameter> UrlParameters { get; set; }

        public EndpointParameter Body { get; set; }

    }
}
