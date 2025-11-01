using OrganizadorTarefa.Models;
using OrganizadorTarefa.Models.DTOs;

namespace OrganizadorTarefa.Services;

public interface ITarefaService
{ 
    Task<List<TarefaReadDto>> GetAllAsync();
    Task<Tarefa?> GetByIdAsync(int id);
    Task<List<TarefaReadDto>> GetByStatusAsync(EnumStatusTarefa statusTarefa);
    Task<List<TarefaReadDto>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<List<TarefaReadDto>> GetByTitleAsync(string title);
    Task<TarefaReadDto> CreateAsync(TarefaCreateDto dto);
    Task<TarefaReadDto?> UpdateAsync(int id, TarefaUpdateDto dto);
    Task DeleteAsync(int id);
    
}
