using System;
using System.ComponentModel.DataAnnotations;

namespace OrganizadorTarefa.Models;

public class Tarefa
{
	
    public int Id { get; set; }	
    public string? Titulo { get; set; }
    public string? Descricao { get; set; }
    public EnumStatusTarefa Status { get; set; }
    public DateTime Data { get; set; }

}
