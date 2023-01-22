using Entities.Entities;

namespace Domain.Interfaces.InterfaceServices
{
    public interface IServiceCompraUsuario
    {
        Task<CompraUsuario> CarrinhoCompras(string userId);
        Task<CompraUsuario> ProdutosComprados(string userId);   
    }
}
