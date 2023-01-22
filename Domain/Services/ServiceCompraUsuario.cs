using Domain.Interfaces.InterfaceCompraUsuario;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;

namespace Domain.Services
{
    public class ServiceCompraUsuario : IServiceCompraUsuario
    {
        private readonly ICompraUsuario _compraUsuario;

        public ServiceCompraUsuario(ICompraUsuario compraUsuario)
        {
            _compraUsuario = compraUsuario;
        }

        public  async Task<CompraUsuario> CarrinhoCompras(string userId)
        {
            return  await _compraUsuario.ProdutosCompradosPorEstado(userId,Entities.Entities.Enuns.EstadoCompra.Produto_Carrinho);
        }

        public async Task<CompraUsuario> ProdutosComprados(string userId)
        {
            return await _compraUsuario.ProdutosCompradosPorEstado(userId, Entities.Entities.Enuns.EstadoCompra.Produto_Comprado);
        }
    }
}
