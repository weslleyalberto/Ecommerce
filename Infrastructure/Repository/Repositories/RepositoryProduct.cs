using Domain.Interfaces.InterfaceProduct;
using Entities.Entities;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository.Repositories
{
    public class RepositoryProduct : RepositoryGenerics<Produto>, IProduct
    {
        private readonly DbContextOptions<ContextBase> _ooptionsBuilder;
        public RepositoryProduct() 
        {
            _ooptionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<List<Produto>> ListarProdutos(Expression<Func<Produto, bool>> exProduto)
        {
            using (var banco = new ContextBase(_ooptionsBuilder))
            {
                return await banco.Produtos.Where(exProduto).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<Produto>> ListarProdutosUsuario(string userId)
        {
            using (var banco = new ContextBase(_ooptionsBuilder))
            {
                return await banco.Produtos.Where(c=> c.UserId== userId).AsNoTracking().ToListAsync();    
            }
        }
    }
}
