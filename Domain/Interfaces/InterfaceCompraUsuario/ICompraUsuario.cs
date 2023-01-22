using Domain.Interfaces.Generics;
using Entities.Entities;
using Entities.Entities.Enuns;

namespace Domain.Interfaces.InterfaceCompraUsuario
{
    public interface ICompraUsuario : IGeneric<CompraUsuario>
    {
        Task<int> QuantidadeProdutoCarrinhoUsuario(string userId);

        Task<CompraUsuario> ProdutosCompradosPorEstado(string userId, EstadoCompra estado);
        Task<bool> ConfirmaCompraCarrinhoUsuario(string userId);


    }
}
