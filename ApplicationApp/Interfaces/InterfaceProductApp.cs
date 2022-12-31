using Entities.Entities;

namespace ApplicationApp.Interfaces
{
    public interface InterfaceProductApp : InterfaceGenericaApp<Produto>
    {
        Task AddProduct(Produto produto);
        Task UpdateProduct(Produto produto);
        Task<List<Produto>> ListarProdutosUsuario(string userId);

        Task<List<Produto>> ListaProdutoComEstoque();
    }
}
