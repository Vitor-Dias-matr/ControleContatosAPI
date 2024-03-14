using ControleContatosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleContatosAPI.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : 
            base(options) { }

        public DbSet<ContatoModel> Contatos { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }
    }
}