using Entities.Entities;
using System.Linq.Expressions;

namespace Domain.Interfaces.InterfaceServices
{
    public interface IServiceProduct
    {
        Task AddProduct(Produto produto);
        Task UpdateProduct(Produto produto);

        Task<List<Produto>> ListarProdutosComEstoque();
    }
}
