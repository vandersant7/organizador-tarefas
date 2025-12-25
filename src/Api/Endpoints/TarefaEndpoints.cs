
using OrganizadorTarefa.Models;
using OrganizadorTarefa.Models.DTOs;
using OrganizadorTarefa.Services;

namespace OrganizadorTarefa.Endpoints;

public static class TarefaEndpoints
{

    public static void MapTarefaEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/tarefas");

        group.MapGet("", async (ITarefaService service) =>
            await service.GetAllAsync())
            .WithName("GetTodas")
            .WithSummary("Obtem todas as tarefas do banco de dados")
            .WithDescription("Retorna a informação sobre as tarefas")
            .WithTags("Tarefas");


        group.MapGet("/{id:int}", async (int id, ITarefaService service) =>
            await service.GetByIdAsync(id) is Tarefa tarefa
                ? Results.Ok(tarefa)
                : Results.NotFound())
            .WithName("GetPorId")
            .WithTags("Tarefas");

        group.MapGet("/status/{status}", async (EnumStatusTarefa status, ITarefaService service) =>
        {
            var tarefas = await service.GetByStatusAsync(status);
            return Results.Ok(tarefas);
        });

        group.MapGet("/datas", async (DateTime inicio, DateTime fim, ITarefaService service) =>
        {
            var tarefas = await service.GetByDateRangeAsync(inicio, fim);
            return Results.Ok(tarefas);
        });

        group.MapGet("/titulo/{title}", async (string title, ITarefaService service) =>
        {
            var tarefas = await service.GetByTitleAsync(title);
            return Results.Ok(tarefas);
        });

        group.MapPost("", async (TarefaCreateDto tarefa, ITarefaService service) =>
        {
            try
            {
                var createdTarefa = await service.CreateAsync(tarefa);
                return Results.CreatedAtRoute("GetPorId", new { id = createdTarefa.Id }, createdTarefa);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        })
            .WithName("Criar")
            .WithTags("Tarefas");

        group.MapPut("{id:int}", async (int id, TarefaUpdateDto tarefa, ITarefaService service) =>
        {
            try
            {
              await service.UpdateAsync(id, tarefa);
              return Results.NoContent();
            }
            catch (KeyNotFoundException) { return Results.NotFound(); }
            catch (ArgumentException ex) { return Results.BadRequest(ex.Message); }
        })
            .WithName("Atualizar")
            .WithTags("Tarefas");

        group.MapDelete("{id:int}", async (int id, ITarefaService service) =>
        {
            try
            {
                await service.DeleteAsync(id);
                return Results.NoContent();
            }
            catch (KeyNotFoundException) { return Results.NotFound(); }
        })
            .WithName("Deletar")
            .WithTags("Tarefas");
    }
}
