using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MVC_Project.Models;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.ViewModel
{
    public class AddProdcutviewModel : ProductViewModel
    {

        [Remote("checkImage", "Product", ErrorMessage = "please enter a valid image")]
        public string? image { get; set; }

    }
}
