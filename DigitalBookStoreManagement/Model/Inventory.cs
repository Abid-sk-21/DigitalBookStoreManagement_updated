using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DigitalBookStoreManagement.Model
{
    public class Inventory
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InventoryID { get; set; }

        [Required(ErrorMessage = "BookID is required")]
        [ForeignKey("BookID")]
        public int BookID { get; set; }

        //[ForeignKey("BookID")]
        //public Book Book { get; set; } // Navigation property

        [Required(ErrorMessage = "Quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "NotifyLimit is required")]
        [Range(1, int.MaxValue, ErrorMessage = "NotifyLimit must be at least 1")]
        public int NotifyLimit { get; set; }

        //[JsonIgnore]
        public BookManagement BookManagement { get; set; }
    }
}
