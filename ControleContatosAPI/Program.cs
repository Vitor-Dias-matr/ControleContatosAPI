using ControleContatosAPI.Data;
using ControleContatosAPI.Mappings;
using ControleContatosAPI.Repositorios;
using ControleContatosAPI.Repositorios.Interaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace ControleContatosAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<BancoContext>
                (options => options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

            builder.Services.AddControllers().AddJsonOptions
                (x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            builder.Services.AddScoped<IContatoRepositorio, ContatoRepositorio>();
            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

            builder.Services.AddAutoMapper(typeof(ModelToViewModel));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}