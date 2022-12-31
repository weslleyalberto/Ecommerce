using Domain.Interfaces.InterfaceCompraUsuario;
using Entities.Entities;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Repositories
{
    public class RepositoryCompraUsuario : RepositoryGenerics<CompraUsuario>, ICompraUsuario
    {
        private readonly DbContextOptions<ContextBase> _optionsBuilder;

        public RepositoryCompraUsuario()
        {
            _optionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<int> QuantidadeProdutoCarrinhoUsuario(string userId)
        {
            using (var data = new ContextBase(_optionsBuilder))
            {
                return await data.CompraUsuarios.CountAsync(c=> c.ApplicationUserId.Equals(userId));
            }
        }
    }
}
