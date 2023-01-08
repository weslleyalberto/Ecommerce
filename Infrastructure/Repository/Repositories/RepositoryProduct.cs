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

        public async Task<List<Produto>> ListarProdutosCarrinhoUsuario(string userId)
        {
            using (var banco = new ContextBase(_ooptionsBuilder))
            {
                var produtoCarrinho = await (from p in banco.Produtos
                                       join c in banco.CompraUsuarios on p.Id equals c.IdProduto
                                       where c.ApplicationUserId.Equals(userId) && c.Estado == Entities.Entities.Enuns.EstadoCompra.Produto_Carrinho
                                       select new Produto
                                       {
                                           Id= p.Id,
                                           Nome = p.Nome,
                                           Descricao = p.Descricao,
                                           Observacao= p.Observacao,
                                           Valor = p.Valor,
                                           QtdCompra = c.QtdCompra,
                                           IdProdutoCarrinho = c.Id,
                                           Url = p.Url
                                       }).AsNoTracking().ToListAsync();
                return produtoCarrinho; 
            }
        }
        public async Task<Produto> ObterProdutoCarrinho(int idProduto)
        {
            using (var banco = new ContextBase(_ooptionsBuilder))
            {
                var produtoCarrinho = await(from p in banco.Produtos
                                            join c in banco.CompraUsuarios on p.Id equals c.IdProduto
                                            where c.Id.Equals(idProduto) && c.Estado == Entities.Entities.Enuns.EstadoCompra.Produto_Carrinho
                                            select new Produto
                                            {
                                                Id = p.Id,
                                                Nome = p.Nome,
                                                Descricao = p.Descricao,
                                                Observacao = p.Observacao,
                                                Valor = p.Valor,
                                                QtdCompra = c.QtdCompra,
                                                IdProdutoCarrinho = c.Id,
                                                Url = p.Url
                                            }).AsNoTracking().FirstOrDefaultAsync();
                return produtoCarrinho;
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
