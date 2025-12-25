using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using OrganizadorTarefa.Models;

namespace OrganizadorTarefa.Data
{
    public class TarefaContext : DbContext
    {
        public TarefaContext(DbContextOptions<TarefaContext> options) : base(options)
        {
        }
        public DbSet<Tarefa> Tarefas => Set<Tarefa>();

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Tarefa>().HasData(
            new Tarefa { Id = 1, Titulo = "Comprar leite", Descricao = "Supermercado", Status = EnumStatusTarefa.Pendente },
            new Tarefa { Id = 2, Titulo = "Estudar .NET", Descricao = "Minimal APIs", Status = EnumStatusTarefa.EmAndamento },
            new Tarefa { Id = 3, Titulo = "Correr", Descricao = "Parque", Status = EnumStatusTarefa.Concluida }
        );
        }
    }
}
