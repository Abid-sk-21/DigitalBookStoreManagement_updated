using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DigitalBookStoreManagement.Model
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(50, ErrorMessage = "CategoryID cannot exceed 50 characters")]
        public string CategoryID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "CategoryName cannot exceed 50 characters")]
        public string CategoryName { get; set; }
    }
}
