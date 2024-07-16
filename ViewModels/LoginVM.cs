using System.ComponentModel.DataAnnotations;

namespace dotnet60_example.ViewModels
{
    public class LoginVM
    {
        public string? UserId { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
