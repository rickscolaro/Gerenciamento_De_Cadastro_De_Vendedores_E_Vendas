

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProjetoSalesWebMvc.Models;
using ProjetoSalesWebMvc.Models.ViewModels;
using ProjetoSalesWebMvc.Services;
using ProjetoSalesWebMvc.Services.Exceptions;

namespace ProjetoSalesWebMvc.Controllers {


    public class SellersController : Controller {

        // Injetando os Serviços
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService) {

            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index() {

            var list = await _sellerService.FindAllAsync();

            return View(list);
        }

        //Create Get
        //[HttpGet]
        public async Task<IActionResult> Create() {

            var departments = await _departmentService.FindAllAsync();// Buscar do banco de dados todos os departamentos
            var viewModel = new SellerFormViewModel { Departments = departments }; // Ja inicia com a lista de  Departments preenchida em um dos dois campos

            return View(viewModel); // Passa a o ViewModel pela View. Quando a tela de cadastro for acionada pela primeira vez ja vai receber uma lista de departamentos
        }

        // Create Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller) {

            
            if (!ModelState.IsValid) {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        // Delete Get
        // Abrir a Tela do Delete
        public async Task<IActionResult> Delete(int? id) {

            if (id == null) {

                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);// Colocar .value porque definimos ele nullable(int? id)

            if (obj == null) {

                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        // Delete Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id) {

            try {

                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));

            } catch (IntegrityException e) {

                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }


        public async Task<IActionResult> Details(int? id) {

            if (id == null) {

                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);// Colocar .value porque definimos ele nullable(int? id)

            if (obj == null) {

                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        // Edit Get
        // Abrir a Tela do Edit
        public async Task<IActionResult> Edit(int? id) {

            if (id == null) {

                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null) {

                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            // Listando os departamentos na tela
            List<Department> departments = await _departmentService.FindAllAsync();
            // Seller = obj, Ja preencho o formulario com os dados o objeto existente 
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        // Edit Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller) {

            
            if (!ModelState.IsValid) {

                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            
            if (id != seller.Id) {

                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try {

                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));

            } catch (ApplicationException e) {

                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

        }

        public IActionResult Error(string message) {

            var viewModel = new ErrorViewModel {

                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier // Macete para pegar o Id interno da Requisição
            };
            return View(viewModel);

        }
    }
}