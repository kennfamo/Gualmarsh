using BackEnd.Model;
using BackEnd.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEnd.Model
{
    public class ShoppingCartViewModel
    {
        public IEnumerable<ShoppingCart>? ShoppingCartListViewModel { get; set; }
        public double CartTotalViewModel { get; set; }
    }
}
