using Domain.DTOs.Eventos;
using System;
using System.Collections.Generic;

public class EventoDto
{
    public int? Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public string Lugar { get; set; } = null!;
    public DateTime Fecha { get; set; }
    public TimeSpan Hora { get; set; }
    public string? ImagenUrl { get; set; }
    public int AforoMaximo { get; set; }
    public DateTime FechaInicioBoleteria { get; set; }
    public DateTime FechaCierreBoleteria { get; set; }
    public List<CategoriaEntradaDto> CategoriasEntrada { get; set; } = new();
}