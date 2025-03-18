using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DigitalBookStoreManagement.Model
{
    public class Review
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReviewID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int BookID { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5 stars.")]
        public int Rating { get; set; }
        [Required]
        [MaxLength(500, ErrorMessage = "Cannot exceed 500 characters")]
        public string Comment { get; set; }
        public bool IsApproved { get; set; } = false;
    }
}
