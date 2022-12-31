using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceProduct;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System;

namespace ApplicationApp.OpenApp
{
    public class AppProduct : InterfaceProductApp
    {

        private readonly IProduct _product;
        private readonly IServiceProduct _serviceProduct;
        public AppProduct(IProduct product, IServiceProduct serviceProduct)
        {
            _product = product;
            _serviceProduct = serviceProduct;
        }

        public async Task Add(Produto objeto)
        {
            await _product.Add(objeto); 
        }

        public async Task AddProduct(Produto produto)
        {
            await _serviceProduct.AddProduct(produto);
        }
        public async Task UpdateProduct(Produto produto)
        {
            await _serviceProduct.UpdateProduct(produto);
        }
        public async Task Delete(Produto objeto)
        {
            await _product.Delete(objeto);
        }

        public async Task<Produto> GetEntityById(int id)
        {
           return await _product.GetEntityById(id);
        }

        public async Task<List<Produto>> List()
        {
            return await _product.List();
        }

        public async Task Update(Produto objeto)
        {
            await _product.Update(objeto);
        }

        public async Task<List<Produto>> ListarProdutosUsuario(string userId)
        {
           return  await _product.ListarProdutosUsuario(userId);
        }

        public async Task<List<Produto>> ListaProdutoComEstoque()
        {
            return await _serviceProduct.ListarProdutosComEstoque();
        }
    }
}
