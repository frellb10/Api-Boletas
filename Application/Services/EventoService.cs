using Application.Interface;
using Application.Interface.IService;
using Domain.DTOs.Eventos;
using Domain.Entities.Eventos;
using Microsoft.EntityFrameworkCore;

public class EventoService : IEventoService
{
    private readonly IEventoRepository _eventoRepository;

    public EventoService(IEventoRepository eventoRepository)
    {
        _eventoRepository = eventoRepository;
    }

    public async Task<IEnumerable<EventoDto>> ObtenerEventosAsync()
    {
        var eventos = await _eventoRepository.ObtenerEventosAsync();
        return eventos.Select(MapToDto);
    }

    public async Task<EventoDto?> ObtenerEventoPorIdAsync(int id)
    {
        var evento = await _eventoRepository.ObtenerEventoPorIdAsync(id);
        return evento == null ? null : MapToDto(evento);
    }

    public async Task<EventoDto> CrearEventoAsync(EventoDto eventoDto)
    {
        var entidad = MapToEntity(eventoDto);
        var creado = await _eventoRepository.CrearEventoAsync(entidad);
        return MapToDto(creado);
    }

    public async Task<EventoDto?> EditarEventoAsync(int id, EventoDto eventoDto)
    {
        var entidad = MapToEntity(eventoDto);
        entidad.Id = id;
        var actualizado = await _eventoRepository.EditarEventoAsync(entidad);
        return actualizado == null ? null : MapToDto(actualizado);
    }

    public async Task<bool> EliminarEventoAsync(int id)
    {
        return await _eventoRepository.EliminarEventoAsync(id);
    }

    // Mapeo de entidad a DTO
    private EventoDto MapToDto(Evento evento)
    {
        return new EventoDto
        {
            Id = evento.Id,
            Nombre = evento.Nombre,
            Descripcion = evento.Descripcion,
            Lugar = evento.Lugar,
            Fecha = evento.Fecha,
            Hora = evento.Hora,
            ImagenUrl = evento.ImagenUrl,
            AforoMaximo = evento.AforoMaximo,
            FechaInicioBoleteria = evento.FechaInicioBoleteria,
            FechaCierreBoleteria = evento.FechaCierreBoleteria,
            CategoriasEntrada = evento.CategoriasEntrada.Select(c => new CategoriaEntradaDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Precio = c.Precio
            }).ToList()
        };
    }

    // Mapeo de DTO a entidad
    private Evento MapToEntity(EventoDto dto)
    {
        return new Evento
        {
            Id = dto.Id ?? 0,
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            Lugar = dto.Lugar,
            Fecha = dto.Fecha,
            Hora = dto.Hora,
            ImagenUrl = dto.ImagenUrl,
            AforoMaximo = dto.AforoMaximo,
            FechaInicioBoleteria = dto.FechaInicioBoleteria,
            FechaCierreBoleteria = dto.FechaCierreBoleteria,
            CategoriasEntrada = dto.CategoriasEntrada.Select(c => new CategoriaEntrada
            {
                Id = c.Id ?? 0,
                Nombre = c.Nombre,
                Precio = c.Precio
            }).ToList()
        };
    }
}