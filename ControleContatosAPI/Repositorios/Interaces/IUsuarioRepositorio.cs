using ControleContatosAPI.ViewModel;

namespace ControleContatosAPI.Repositorios.Interaces
{
    public interface IUsuarioRepositorio
    {
        Task<ResponseViewModel> BuscarTodosUsuarios();
        Task<ResponseViewModel> BuscarPorId(int id);
        Task<ResponseViewModel> BuscarPorLogin(string login);
        Task<ResponseViewModel> BuscarPorEmailELogin(string email, string login);
        Task<ResponseViewModel> Adicionar(UsuarioViewModel usuario);
        Task<ResponseViewModel> Atualizar(UsuarioSemSenhaViewModel usuarioSemSenha);
        Task<ResponseViewModel> Apagar(int id);
        Task<ResponseViewModel> AlterarSenhaUsuario(AlterarSenhaViewModel alterarSenha);
    } 
}