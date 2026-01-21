using System.ComponentModel.DataAnnotations;

namespace Bilet_15.ViewModels.Account
{
    public class RegisterVm
    {
       
        [MinLength(3, ErrorMessage = "qisadir")]
        [MaxLength(20, ErrorMessage = "uzundur")]
        public string Name { get; set; }
        [MinLength(3, ErrorMessage = "qisadir")]
        [MaxLength(20, ErrorMessage = "uzundur")]
        public string Surname { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
