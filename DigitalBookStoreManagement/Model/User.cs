using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DigitalBookStoreManagement.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Range(0, 99999999, ErrorMessage = "Negative numbers a not allowed")]/
        public int UserID { get; set; }

        //name
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        [MinLength(3, ErrorMessage = "Name cannot be shorter than 3 characters.")]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Only alphabets are allowed in name.")]
        public string Name { get; set; }

        //email
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters and shorter than 3 characters.")]
        [MinLength(3, ErrorMessage = "email cannot be shorter than 3 characters.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [RegularExpression(@"^[^@\s]+@gmail\.com$", ErrorMessage = "Email must be a Gmail address.")]
        public string Email { get; set; }

        //password
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        [MinLength(3, ErrorMessage = "Name cannot be shorter than 3 characters.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string Password { get; set; }


        //[Required(ErrorMessage = "Confirm Password is required")]
        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //public string ConfirmPassword { get; set; }


        //Role
        [Required]
        [StringLength(10)]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Only alphabets are allowed in name.")]
        [MinLength(3, ErrorMessage = "Name cannot be shorter than 3 characters.")]
        public string Role { get; set; }
    }
}
