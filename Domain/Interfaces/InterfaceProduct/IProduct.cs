using Domain.Interfaces.Generics;
using Entities.Entities;
using System.Linq.Expressions;

namespace Domain.Interfaces.InterfaceProduct
{
    public interface IProduct : IGeneric<Produto>
    {
        Task<List<Produto>> ListarProdutosUsuario(string userId);
        Task<List<Produto>> ListarProdutos(Expression<Func<Produto, bool>> exProduto);

    }
}
