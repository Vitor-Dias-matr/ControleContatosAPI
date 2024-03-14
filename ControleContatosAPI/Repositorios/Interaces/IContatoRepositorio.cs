using ControleContatosAPI.ViewModel;

namespace ControleContatosAPI.Repositorios.Interaces
{
    public interface IContatoRepositorio
    {
        Task<ResponseViewModel> BuscarTodosContatos();
        Task<ResponseViewModel> BuscarContatosDeUsuario(int usuarioId);
        Task<ResponseViewModel> BuscarPorId(int id);
        Task<ResponseViewModel> Adicionar(ContatoViewModel contato);
        Task<ResponseViewModel> Atualizar(ContatoViewModel contato);
        Task<ResponseViewModel> Apagar(int id);
    }
}