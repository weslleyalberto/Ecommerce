using ApplicationApp.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web_ECommerce.Controllers
{
    public class CompraUsuarioController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly InterfaceCompraUsuarioApp _interfaceCompraUsuarioApp;
        private readonly InterfaceProductApp _interfaceProductApp;

        public CompraUsuarioController(UserManager<ApplicationUser> userManager, InterfaceCompraUsuarioApp interfaceCompraUsuarioApp, InterfaceProductApp interfaceProductApp)
        {
            _userManager = userManager;
            _interfaceCompraUsuarioApp = interfaceCompraUsuarioApp;
            _interfaceProductApp = interfaceProductApp;
        }
        [HttpPost("/api/AdicionarProdutoCarrinho")]
        public async Task<JsonResult> AdicionarProdutoCarrinho(string id, string nome, string qtd)
        {
            var usuario = await _userManager.GetUserAsync(User);
            if(usuario is not null)
            {
                //Produto produto = await _interfaceProductApp.GetEntityById(Convert.ToInt32(id));
               
                await _interfaceCompraUsuarioApp.Add(new CompraUsuario
                {
                   IdProduto = Convert.ToInt32(id),
                    QtdCompra = Convert.ToInt32(qtd),
                    Estado = Entities.Entities.Enuns.EstadoCompra.Produto_Caminho,
                  
                     ApplicationUserId = usuario.Id,


                }) ;
              
                return Json(new { sucesso = true });
            }
            return Json(new { sucesso = false });
        }
        [HttpGet("/api/QtdProdutoCarrinho")]
        public async Task<JsonResult> QtdProdutoCarrinho()
        {
            var usuario = await _userManager.GetUserAsync(User);
            int qtd = 0;
            if(usuario is not null)
            {
               qtd = await  _interfaceCompraUsuarioApp.QuantidadeProdutoCarrinhoUsuario(usuario.Id);

                return Json(new {sucesso = true,qtd = qtd});
            }
            return Json(new {sucesso = false, qtd = qtd});
        }

    }
}
