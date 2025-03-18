using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DigitalBookStoreManagement.Authentication;

namespace DigitalBookStoreManagement.Model
{
    public class BookManagement
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BookID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; set; }

        public int AuthorID { get; set; }

        public string CategoryID { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 99999.99, ErrorMessage = "Price must be between 0.01 and 99,999.99.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public string StockQuantity { get; set; }

        [Required]
        [Url]
        public string ImageURL { get; set; }

        [ForeignKey("AuthorID")]
        public Author Author { get; set; }

        [ForeignKey("CategoryID")]
        public Category Category { get; set; }
    }
}
