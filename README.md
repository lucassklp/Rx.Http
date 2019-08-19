# Rx.Http

This library is inpired in [Angular Http Client](https://angular.io/guide/http) and help programmers to make asynchronous http requests.

**Note: This project is under development.**

## Example of use:

```csharp
using Rx.Http;
using System.Reactive.Linq;

//Retrieve a list of To-Do item and print the title of each element asynchronously
request.Get<List<Todo>>("https://jsonplaceholder.typicode.com/todos/").Subscribe(itens => {
    itens.ForEach(item => Console.WriteLine(item.title));
});

//Making the same request using await
List<Todo> response = await request.Get<List<Todo>>("https://jsonplaceholder.typicode.com/todos/");

```
