using Domain.Interfaces.InterfaceCompraUsuario;
using Entities.Entities;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Repositories
{
    public class RepositoryCompraUsuario : RepositoryGenerics<CompraUsuario>, ICompraUsuario
    {
       
    }
}
