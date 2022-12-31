using ApplicationApp.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web_ECommerce.Controllers
{
    [Authorize]
    public class ProdutosController : Controller
    {
        private readonly InterfaceProductApp _interfaceProductApp;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProdutosController(InterfaceProductApp interfaceProductApp,UserManager<ApplicationUser> userManager)
        {
            _interfaceProductApp = interfaceProductApp;
            _userManager = userManager;
        }

        // GET: ProdutosController
        public async  Task<IActionResult> Index()
        {
           
            return View(await _interfaceProductApp.ListarProdutosUsuario(await RetornarIdUsuarioLogado()));
        }

        // GET: ProdutosController/Details/5
        public  async Task<IActionResult> Details(int id)
        {
            return View(await _interfaceProductApp.GetEntityById(id));
        }

        // GET: ProdutosController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: ProdutosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produto produto)
        {
            try
            {
                var idUsuario = await RetornarIdUsuarioLogado();
              //  produto.ApplicationUsers.Id = idUsuario;
               // produto.ApplicationUsers.Id= idUsuario;
                produto.UserId= idUsuario;
               await _interfaceProductApp.AddProduct(produto);
                if (produto.Notitycoes.Any())
                {
                    foreach (var item in produto.Notitycoes)
                    {
                        ModelState.AddModelError(item.NomePropriedade, item.message);
                    }
                    //TODO -- Se de error trocar para retornar view Edit


                    return View(nameof(Create), produto);
                }
               
               
            }
            catch
            {
                //TODO -- Se de error trocar para retornar view Edit
                return View(nameof(Create), produto);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ProdutosController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _interfaceProductApp.GetEntityById(id));
        }

        // POST: ProdutosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Produto produto)
        {
            try
            {
                await _interfaceProductApp.UpdateProduct(produto);
                if (produto.Notitycoes.Any())
                {
                     foreach(var item in produto.Notitycoes)
                    {
                        ModelState.AddModelError(item.NomePropriedade, item.message);
                    }
                   ViewBag.Alerta = true;
                    ViewBag.Mensagem = "Verifique! Ocorreu algum erro!";

                     return View(nameof(Edit), produto);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(nameof(Edit), produto);
            }
        }

        // GET: ProdutosController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _interfaceProductApp.GetEntityById(id));
        }

        // POST: ProdutosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Produto produto)
        {
            try
            {
                var produtoDeletar = await _interfaceProductApp.GetEntityById(id);
                await _interfaceProductApp.Delete(produtoDeletar);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpGet("/api/ListaProdutoComEstoque")]
        [AllowAnonymous]
        public async Task<JsonResult> ListaProdutoComEstoque()
        {
            return Json(await _interfaceProductApp.ListaProdutoComEstoque());
        }
       
        private async Task<string> RetornarIdUsuarioLogado()
        {
            var idUsuario = await _userManager.GetUserAsync(User);
            return idUsuario.Id;
        }
    }
}
