namespace OrganizadorTarefa.Models.DTOs
{
    public class TarefaCreateDto
    {
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public EnumStatusTarefa Status { get; set; } = EnumStatusTarefa.Pendente;
    }
}
