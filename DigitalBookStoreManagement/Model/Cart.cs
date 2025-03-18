using System.ComponentModel.DataAnnotations;

namespace DigitalBookStoreManagement.Model
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }

        [Required(ErrorMessage = "UserId is Required.")]
        public int UserID { get; set; }


        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        //[Required(ErrorMessage = "Status is Required.")]
        //public string status { get; set; }

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
