using ControleContatosAPI.Enums;

namespace ControleContatosAPI.Models
{
    public class UsuarioSemSenhaModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public PerfilEnum? Perfil { get; set; }
    }
}