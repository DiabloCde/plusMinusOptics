using System.ComponentModel.DataAnnotations; 
using Microsoft.AspNetCore.Http; 
using PlusMinus.Core.Models.Enumerations; 
 
namespace PlusMinus.ViewModels 
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Lastname { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public IFormFile Receipt { get; set; }

        public string ImageUrl { get; set; }
    }
}