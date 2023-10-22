using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVC_Project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.ViewModel.OrderViewModels
{
    public class AllOrderHistoryVM
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public int Status { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }
    }
}
