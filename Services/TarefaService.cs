using Microsoft.EntityFrameworkCore;
using OrganizadorTarefa.Data;
using OrganizadorTarefa.Models;
using OrganizadorTarefa.Models.DTOs;

namespace OrganizadorTarefa.Services;

public class TarefaService : ITarefaService
{
    private readonly TarefaContext _context;

    public TarefaService(TarefaContext context)
    {
        _context = context;
    }

    public async Task<List<TarefaReadDto>> GetAllAsync()
    {
        var todasTarefas = await _context.Tarefas.ToListAsync();
        if (todasTarefas.Count == 0)
           return "Nenhuma tarefa encontrada.".Select(_ => new TarefaReadDto()).ToList();

        return await _context.Tarefas
            .Select(t => new TarefaReadDto
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descricao = t.Descricao,
                Status = t.Status,
                Data = t.Data
            })
            .ToListAsync();
    }

    public async Task<Tarefa?> GetByIdAsync(int id)
    {
        return await _context.Tarefas.FindAsync(id);
    }

    public async Task<TarefaReadDto> CreateAsync(TarefaCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Titulo))
            throw new ArgumentException("O título é obrigatório.");

        var tarefa = new Tarefa
        {
            Titulo = dto.Titulo,
            Descricao = dto.Descricao,
            Status = dto.Status,
            Data = DateTime.Now
        };

        _context.Tarefas.Add(tarefa);
        await _context.SaveChangesAsync();

        var tarefaReadDto = new TarefaReadDto
        {
            Id = tarefa.Id,
            Titulo = tarefa.Titulo,
            Descricao = tarefa.Descricao,
            Status = tarefa.Status,
            Data = tarefa.Data
        };

        return tarefaReadDto;
    }

    public async Task<TarefaReadDto?> UpdateAsync(int id, TarefaUpdateDto dto)
    {
        var tarefa = await _context.Tarefas.FindAsync(id) ?? 
            throw new KeyNotFoundException("Tarefa não encontrada.");

        if (string.IsNullOrWhiteSpace(dto.Titulo))
            throw new ArgumentException("O título é obrigatório.");

        tarefa.Titulo = dto.Titulo;
        tarefa.Descricao = dto.Descricao;
        tarefa.Status = dto.Status;

        await _context.SaveChangesAsync();

        return new TarefaReadDto
        {
            Id = tarefa.Id,
            Titulo = tarefa.Titulo,
            Descricao = tarefa.Descricao,
            Status = tarefa.Status,
            Data = tarefa.Data
        };
    }

    public async Task DeleteAsync(int id)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);

        if (tarefa is null)
            throw new KeyNotFoundException("Tarefa não encontrada.");

        _context.Tarefas.Remove(tarefa);
        await _context.SaveChangesAsync();

    }

    public async Task<List<TarefaReadDto>> GetByStatusAsync(EnumStatusTarefa statusTarefa)
    {
        return await _context.Tarefas
            .Where(t => t.Status == statusTarefa)
            .Select(t => new TarefaReadDto
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descricao = t.Descricao,
                Status = t.Status,
                Data = t.Data
            })
            .ToListAsync();
    }

    // 🔹 2. Buscar por intervalo de datas
    public async Task<List<TarefaReadDto>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Tarefas
            .Where(t => t.Data >= startDate && t.Data <= endDate)
            .Select(t => new TarefaReadDto
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descricao = t.Descricao,
                Status = t.Status,
                Data = t.Data
            })
            .ToListAsync();
    }

    // 🔹 3. Buscar por título (contém)
    public async Task<List<TarefaReadDto>> GetByTitleAsync(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return new List<TarefaReadDto>();

        return await _context.Tarefas
            .Where(t => t.Titulo!.ToLower().Contains(title.ToLower()))
            .Select(t => new TarefaReadDto
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descricao = t.Descricao,
                Status = t.Status,
                Data = t.Data
            })
            .ToListAsync();
    }

}
