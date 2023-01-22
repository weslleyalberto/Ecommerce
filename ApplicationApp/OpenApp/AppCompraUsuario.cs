using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceCompraUsuario;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;

namespace ApplicationApp.OpenApp
{
    public class AppCompraUsuario : InterfaceCompraUsuarioApp
    {
        private readonly ICompraUsuario _compraUsuario;
        private readonly IServiceCompraUsuario _serviceCompraUsuario;

        public AppCompraUsuario(ICompraUsuario compraUsuario,IServiceCompraUsuario serviceCompraUsuario)
        {
            _compraUsuario = compraUsuario;
            _serviceCompraUsuario = serviceCompraUsuario;
        }
        public async Task<int> QuantidadeProdutoCarrinhoUsuario(string userId)
        {
            return await _compraUsuario.QuantidadeProdutoCarrinhoUsuario(userId);
        }
        public async Task Add(CompraUsuario objeto)
        {
            await _compraUsuario.Add(objeto);
        }

        public async Task Delete(CompraUsuario objeto)
        {
            await _compraUsuario.Delete(objeto);
        }

        public async Task<CompraUsuario> GetEntityById(int id)
        {
           return await _compraUsuario.GetEntityById(id);
        }

        public async Task<List<CompraUsuario>> List()
        {
            return await _compraUsuario.List();
        }

      

        public async Task Update(CompraUsuario objeto)
        {
           await _compraUsuario.Update(objeto);
        }

        public async Task<CompraUsuario> CarrinhoCompras(string userId)
        {
            return await _serviceCompraUsuario.CarrinhoCompras(userId);
        }

        public async Task<CompraUsuario> ProdutosComprados(string userId)
        {
            return await _serviceCompraUsuario.ProdutosComprados(userId);
        }

        public async Task<bool> ConfirmaCompraCarrinhoUsuario(string userId)
        {
            return await _compraUsuario.ConfirmaCompraCarrinhoUsuario(userId);
        }
    }
}
