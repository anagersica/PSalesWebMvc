using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PSalesWebMvc.Services;

namespace PSalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        //Essa dependência do SellerService chama o  FindAll
        private readonly SellerService _sellerService;
        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }
        public IActionResult Index()//controlador acessou o model e encaminhou pra view
        {
            var list = _sellerService.FindAll();//model
            return View(list);//view
        }
    }
}
