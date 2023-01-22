using Domain.Interfaces.InterfaceCompraUsuario;
using Entities.Entities;
using Entities.Entities.Enuns;
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

        public async Task<bool> ConfirmaCompraCarrinhoUsuario(string userId)
        {
            try
            {
                using (var banco = new ContextBase(_optionsBuilder))
                {
                    var compraUsuario = new CompraUsuario();
                    compraUsuario.ListaProdutos = new List<Produto>();

                    var produtoCarrinhoUsuario = await (from p in banco.Produtos
                                                        join c in banco.CompraUsuarios on p.Id equals c.IdProduto
                                                        where c.ApplicationUserId.Equals(userId) && c.Estado == EstadoCompra.Produto_Carrinho
                                                        select c).AsNoTracking().ToListAsync();

                    produtoCarrinhoUsuario.ForEach(p =>
                    {
                        p.Estado = EstadoCompra.Produto_Comprado;

                    });
                    banco.UpdateRange(produtoCarrinhoUsuario);
                    await banco.SaveChangesAsync();

                    return true;

                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            
        }

        public async Task<CompraUsuario> ProdutosCompradosPorEstado(string userId, EstadoCompra estado)
        {
            using (var banco = new ContextBase(_optionsBuilder))
            {
                var compraUsuario = new CompraUsuario();
                compraUsuario.ListaProdutos = new List<Produto>();

                var produtoCarrihoUsuario = await (from p in banco.Produtos
                                                   join c in banco.CompraUsuarios on p.Id equals c.IdProduto
                                                   where c.ApplicationUserId.Equals(userId) && c.Estado == estado
                                                   select new Produto
                                                   {
                                                       Id = p.Id,
                                                       Nome = p.Nome,
                                                       Descricao = p.Descricao,
                                                       Observacao = p.Observacao,
                                                       Valor = p.Valor,
                                                       QtdCompra = p.QtdCompra,
                                                       IdProdutoCarrinho = c.Id,
                                                       Url = p.Url,

                                                   }).AsNoTracking().ToListAsync();

                compraUsuario.ListaProdutos = produtoCarrihoUsuario;
                compraUsuario.ApplicationUser = await banco.ApplicationUser.FirstOrDefaultAsync(u => u.Id.Equals(userId));
                compraUsuario.QuantidadeProdutos = produtoCarrihoUsuario.Count;
                compraUsuario.EnderecoCompleto = string.Concat(compraUsuario.ApplicationUser.Endereco, " - ", compraUsuario.ApplicationUser.Complemento, " - CEP: ", compraUsuario.ApplicationUser.CEP);
                compraUsuario.ValorTotal = produtoCarrihoUsuario.Sum(v => v.Valor);
                compraUsuario.Estado = estado;
                return compraUsuario;


            }

        }

        public async Task<int> QuantidadeProdutoCarrinhoUsuario(string userId)
        {
            using (var data = new ContextBase(_optionsBuilder))
            {
                return await data.CompraUsuarios.CountAsync(c => c.ApplicationUserId.Equals(userId) && c.Estado == Entities.Entities.Enuns.EstadoCompra.Produto_Carrinho);
            }
        }
    }
}
