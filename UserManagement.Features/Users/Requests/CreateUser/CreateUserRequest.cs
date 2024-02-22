using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Features.Users.Requests.CreateUser
{
    public class CreateUserRequest : IRequest<bool>
    {
        [Required(ErrorMessage = "Name of the user is required")]
        [MaxLength(100, ErrorMessage = "Name of the user must contain a maximum of 100 characters.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Email of the user is required")]
        [MaxLength(120, ErrorMessage = "Email of the user must contain a maximum of 120 characters.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Username is required")]
        [MaxLength(20, ErrorMessage = "Username must contain a maximum of 20 characters.")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "CPF is required")]
        [MaxLength(11, ErrorMessage = "CPF must contain a maximum of 11 characters.")]
        public string? CPF { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Birth date is required")]
        public DateTime? BirthDate { get; set; }
    }
}