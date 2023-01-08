using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceCompraUsuario;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security;
using System.Security.Permissions;

namespace Web_ECommerce.Controllers
{
    [Authorize]
    public class ProdutosController : Controller
    {
        private readonly InterfaceProductApp _interfaceProductApp;
        private readonly ICompraUsuario _compraUsuario;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProdutosController(InterfaceProductApp interfaceProductApp,UserManager<ApplicationUser> userManager,ICompraUsuario compraUsuario,IWebHostEnvironment webHostEnvironment)
        {
            _interfaceProductApp = interfaceProductApp;
            _userManager = userManager;
            _compraUsuario= compraUsuario;
            _webHostEnvironment = webHostEnvironment;
            
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
        public  IActionResult Create()
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
                await SalvarImagemProduto(produto); 
               
               
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
       
        public async Task<IActionResult> ListarProdutosCarrinhoUsuario()
        {
            var idUsuario = await RetornarIdUsuarioLogado();

            return View(await _interfaceProductApp.ListarProdutosCarrinhoUsuario(idUsuario));
        }
        public async Task<IActionResult> RemoverCarrinho(int id)
        {
            return View(await _interfaceProductApp.ObterProdutoCarrinho(id));
        }

        // POST: ProdutosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverCarrinho(int id, Produto produto)
        {
            try
            {
                var produtoDeletar = await _compraUsuario.GetEntityById(id);
                await _compraUsuario.Delete(produtoDeletar);

                return RedirectToAction(nameof(ListarProdutosCarrinhoUsuario));
            }
            catch
            {
                return View();
            }
        }
        public async Task SalvarImagemProduto(Produto produtoTela)
        {
            try
            {
                var produto = await _interfaceProductApp.GetEntityById(produtoTela.Id);
                if (produtoTela.Imagem is not null)
                {
                    var webRoot = _webHostEnvironment.WebRootPath;
                    var permissionSet = new PermissionSet(System.Security.Permissions.PermissionState.Unrestricted);
                    var writePermission = new FileIOPermission(FileIOPermissionAccess.Append,
                        string.Concat(webRoot, "/imgProdutos"));
                    permissionSet.AddPermission(writePermission);
                    var extension = Path.GetExtension(produtoTela.Imagem.FileName);
                    var nomeArquivo = string.Concat(produto.Id.ToString(), extension);
                    var diretorioArquivosalvar = string.Concat(webRoot, "\\imgProdutos\\", nomeArquivo);
                    produtoTela.Imagem.CopyTo(new FileStream(diretorioArquivosalvar, FileMode.Create));
                    produto.Url = string.Concat("https://localhost:7136", "/imgProdutos/", nomeArquivo);
                    await _interfaceProductApp.UpdateProduct(produto);
                }
            }
            catch (Exception)
            {
                throw;
            }


           
        }
        private async Task<string> RetornarIdUsuarioLogado()
        {
            var idUsuario = await _userManager.GetUserAsync(User);
            return idUsuario.Id;
        }
    }
}
