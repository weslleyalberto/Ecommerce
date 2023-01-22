using Entities.Entities;

namespace ApplicationApp.Interfaces
{
    public interface InterfaceCompraUsuarioApp : InterfaceGenericaApp<CompraUsuario>
    {
        Task<int> QuantidadeProdutoCarrinhoUsuario(string userId);

        Task<CompraUsuario> CarrinhoCompras(string userId);
        Task<CompraUsuario> ProdutosComprados(string userId);
        Task<bool> ConfirmaCompraCarrinhoUsuario(string userId);
    }
}
