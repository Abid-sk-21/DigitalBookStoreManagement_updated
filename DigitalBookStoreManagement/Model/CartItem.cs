using System.ComponentModel.DataAnnotations;

namespace DigitalBookStoreManagement.Model
{
    public class CartItem
    {
        [Key]
        public int CartItemID { get; set; }

        public int CartID { get; set; }

        [Required(ErrorMessage = "BookId is Required.")]
        public int BookID { get; set; }

        [Required(ErrorMessage = "Price is Required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Quantity is Required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Total Amount is Required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = " Total Amount must be greater than zero")]
        public double TotalAmount { get; set; }
    }
}
