using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PSalesWebMvc.Services;
using PSalesWebMvc.Models;
using PSalesWebMvc.Models.ViewModel;
using PSalesWebMvc.Services.Exceptions;
using System.Diagnostics;

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
            if (!ModelState.IsValid)//caso o form não esteja preenchido corretamente ele não deixa criar
            {
                var departments = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);//volta pro form até que seja preenchido corretamente
            }
            _sellerService.Insert(seller);//vai inserir no BD
            return RedirectToAction(nameof(Index)); //redireciona a requisicao a Index view 
        }
        //ação para deletar seller GET
        public IActionResult Delete(int? id)//a ? indica que o ID é opcional
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
           
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
                
            }
            return View(obj);
        }
        //ação para deletar seller método POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }
        //Edit GET
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = _sellerService.FindById(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            //abrindo tela de edição do Edit
            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
                return View(viewModel);
            }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)//caso o form não esteja preenchido corretamente ele não deixa criar
            {
                var departments = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);//volta pro form até que seja preenchido corretamente
            }
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch(ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            /* Trocando esses dois pelo súper tipo ApplicationException para ficar menos repetitivo
             * catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch(DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            } */

        }
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        };
        return View(viewModel);
        }
    }
}
