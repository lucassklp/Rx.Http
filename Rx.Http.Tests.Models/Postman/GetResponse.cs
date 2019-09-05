using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rx.Http.Tests.Models.Postman
{
    public class GetResponse
    {
        [JsonProperty("args")]
        public Dictionary<string, string> Args { get; set; }

        [JsonProperty("headers")]
        public Dictionary<string, string> Headers { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

    }
}
