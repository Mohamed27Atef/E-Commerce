using System.ComponentModel.DataAnnotations;

namespace E_Commerce.ViewModel.OrderViewModels
{
    public class OrderCheckedUserVM
    {
        public string Street { get; set; }
        [MaxLength(255)]
        public string City { get; set; }
        [MaxLength(255)]
        public string Country { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
