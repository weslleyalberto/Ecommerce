using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceCompraUsuario;
using Entities.Entities;

namespace ApplicationApp.OpenApp
{
    public class AppCompraUsuario : InterfaceCompraUsuarioApp
    {
        private readonly ICompraUsuario _compraUsuario;

        public AppCompraUsuario(ICompraUsuario compraUsuario)
        {
            _compraUsuario = compraUsuario;
        }

        public async Task Add(CompraUsuario objeto)
        {
            await _compraUsuario.Add(objeto);
        }

        public async Task Delete(CompraUsuario objeto)
        {
            await _compraUsuario.Delete(objeto);
        }

        public async Task<CompraUsuario> GetEntityById(int id)
        {
           return await _compraUsuario.GetEntityById(id);
        }

        public async Task<List<CompraUsuario>> List()
        {
            return await _compraUsuario.List();
        }

        public async Task Update(CompraUsuario objeto)
        {
           await _compraUsuario.Update(objeto);
        }
    }
}
