using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PSalesWebMvc.Services;
using PSalesWebMvc.Models;
using PSalesWebMvc.Models.ViewModel;

namespace PSalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        //Essa dependência do SellerService chama o  FindAll
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;
        

        //construtor para injetar a dependência
        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }
        public IActionResult Index()//controlador acessou o model e encaminhou pra view
        {
            var list = _sellerService.FindAll();//model
            return View(list);//view
        }
        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);//chama a funcão criada p o btn Create de Sellers
        }
        [HttpPost] //serve para informar que aação é POST. Pq automaticamente o sistema entende que é GET
        [ValidateAntiForgeryToken]//contra CSRF - Especifica que a classe ou método ao qual este atributo é aplicado valida o token anti-falsificação. Se o token anti-falsificação não estiver disponível ou se o token for inválido, a validação falhará e o método de ação não será executado.
        public IActionResult Create(Seller seller)//recebe o obj da requisição de criar novo funcionario e instancia o vendedor
        {
            _sellerService.Insert(seller);//vai inserir no BD
            return RedirectToAction(nameof(Index)); //redireciona a requisicao a Index view 
        }
    }
}
