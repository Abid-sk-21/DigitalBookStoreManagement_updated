using System.ComponentModel.DataAnnotations;

namespace DigitalBookStoreManagement.Model
{
    public class OrderItem
    {
        [Key]
        public int OrderItemID { get; set; }
        //public Order? Order { get; set; }
        public int OrderID { get; set; }

        [Required(ErrorMessage = "BookId is Required.")]
        public int BookID { get; set; }



        [Required(ErrorMessage = "Price is Required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Quantity is Required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Quantity must be greater than zero")]
        public double Quantity { get; set; }

        [Required(ErrorMessage = "Total Amount is Required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = " Total Amount must be greater than zero")]
        public double TotalAmount { get; set; }

    }
}
