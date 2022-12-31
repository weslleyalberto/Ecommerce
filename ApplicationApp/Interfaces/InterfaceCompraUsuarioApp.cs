using Entities.Entities;

namespace ApplicationApp.Interfaces
{
    public interface InterfaceCompraUsuarioApp : InterfaceGenericaApp<CompraUsuario>
    {
        Task<int> QuantidadeProdutoCarrinhoUsuario(string userId);
    }
}
