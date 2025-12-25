namespace OrganizadorTarefa.Models.DTOs
{
    public class TarefaReadDto
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public EnumStatusTarefa Status { get; set; }
        public DateTime Data { get; set; }
    }
}
