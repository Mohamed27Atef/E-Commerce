using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.ViewModel
{
    public class ProductViewModel
    {

        [Required(ErrorMessage = "name is required")]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required(ErrorMessage = "brand is required")]
        [MaxLength(255)]
        public string Brand { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "price is required")]
        [RegularExpression(@"^\d*\.?\d*$", ErrorMessage = "please enter a valid number")]
        public decimal Price { get; set; }

        [Required]
        [Remote("checkStockQuantity", "Product", ErrorMessage = "invalid count")]
        [Display(Name = "count in the stock")]
        [RegularExpression(@"^[0-9]{1,}$", ErrorMessage = "please enter a valid number")]
        public int StockQuantity { get; set; }

        [Display(Name = "category")]
        public int category_id { get; set; }

    }
}
