using System.Net;

namespace dotnet60_example.ViewModels
{
    public class ResultVM
    {
        public HttpStatusCode StatusCode { get; set; }

        public string? Message { get; set; }

        public object? Body { get; set; }
    }
}
