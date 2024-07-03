using Models;
using Rx.Http;
using Rx.Http.Exceptions;
using Rx.Http.Extensions;
using System.Collections.Generic;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xunit;
using System.IO;

namespace Rx.Http.Tests
{
    public class RequestTests
    {
        private readonly RxHttpClient http;
        public RequestTests()
        {
            http = new RxHttpClient(new HttpClient(), null);
        }


        [Fact]
        public async Task TestGetAsJsonObject()
        {
            var todos = await http.Get<List<Todo>>("https://jsonplaceholder.typicode.com/todos/");

            Assert.NotNull(todos);
        }



        [Fact]
        public async Task TestGet404Error()
        {
            await Assert.ThrowsAsync<RxHttpRequestException>(async () =>
            {
                //Invalid URL
                var url = @"https://jsonplaceholder.typicode.com/this_page_dont_exist_hehehe/";
                await http.Get<List<Todo>>(url);
            });
        }

        [Fact]
        public async Task TestPostWithJson()
        {
            var postWithId = await http.Post<Identifiable>(@"https://jsonplaceholder.typicode.com/posts/", new Post()
            {
                Title = "Foo",
                Body = "Bar",
                UserId = 3
            });

            Assert.NotNull(postWithId);
        }

        [Fact]
        public async Task TestDownloadFile()
        {
            var fileName = $"mysql-installer-web-community-8.0.22.0.msi";
            var directory = Directory.GetCurrentDirectory();
            var path = Path.Combine(directory, fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            await http.Get($@"https://dev.mysql.com/get/Downloads/MySQLInstaller/{fileName}")
                .ToFile(path);

            Assert.True(File.Exists(path));
            File.Delete(path);
        }
    }
}