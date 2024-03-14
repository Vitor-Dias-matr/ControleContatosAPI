using ControleContatosAPI.Enums;

namespace ControleContatosAPI.ViewModel
{
    public class UsuarioSemSenhaViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public PerfilEnum? Perfil { get; set; }
    }
}