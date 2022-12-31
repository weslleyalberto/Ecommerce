using Domain.Interfaces.InterfaceProduct;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System.Linq.Expressions;

namespace Domain.Services
{
    public class ServiceProduto : IServiceProduct
    {
        private readonly IProduct _product;

        public ServiceProduto(IProduct product)
        {
            _product = product;
        }

        public async Task AddProduct(Produto produto)
        {
            var validaNome = produto.ValidarPropriedadeString(produto.Nome,"Nome");
            var validaValor = produto.ValidarPropriedadeDecimal(produto.Valor,"Valor");
            var validaEstoque = produto.ValidarPropriedadeInt(produto.QuantidadeEstoque, "QuantidadeEstoque");

            if(validaNome && validaValor && validaEstoque)
            {
                produto.DataCadastro = DateTime.Now;
                produto.DataAlteracao = DateTime.Now;
                produto.Estado= true;
               await _product.Add(produto);
            }

        }

        public async Task<List<Produto>> ListarProdutosComEstoque()
        {
            return await _product.ListarProdutos(p => p.QuantidadeEstoque >0); 
        }

        public async Task UpdateProduct(Produto produto)
        {
            var validaNome = produto.ValidarPropriedadeString(produto.Nome, "Nome");
            var validaValor = produto.ValidarPropriedadeDecimal(produto.Valor, "Valor");
            var validaEstoque = produto.ValidarPropriedadeInt(produto.QuantidadeEstoque, "QuantidadeEstoque");

            if (validaNome && validaValor && validaEstoque)
            {
                produto.DataAlteracao= DateTime.Now;
                await _product.Update(produto);
            }
        }
    }
}
