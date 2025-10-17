using Application.Interface;
using Domain.Entities.Eventos;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class EventoRepository : IEventoRepository
{
    private readonly ApplicationDbContext _context;

    public EventoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Evento>> ObtenerEventosAsync()
    {
        return await _context.Eventos
            .Include(e => e.CategoriasEntrada)
            .Where(e => e.Activo)
            .ToListAsync();
    }

    public async Task<Evento?> ObtenerEventoPorIdAsync(int id)
    {
        return await _context.Eventos
            .Include(e => e.CategoriasEntrada)
            .FirstOrDefaultAsync(e => e.Id == id && e.Activo);
    }

    public async Task<Evento> CrearEventoAsync(Evento evento)
    {
        _context.Eventos.Add(evento);
        await _context.SaveChangesAsync();
        return evento;
    }

    public async Task<Evento?> EditarEventoAsync(Evento evento)
    {
        var existente = await _context.Eventos
            .Include(e => e.CategoriasEntrada)
            .FirstOrDefaultAsync(e => e.Id == evento.Id && e.Activo);

        if (existente == null) return null;

        existente.Nombre = evento.Nombre;
        existente.Descripcion = evento.Descripcion;
        existente.Lugar = evento.Lugar;
        existente.Fecha = evento.Fecha;
        existente.Hora = evento.Hora;
        existente.ImagenUrl = evento.ImagenUrl;
        existente.AforoMaximo = evento.AforoMaximo;
        existente.FechaInicioBoleteria = evento.FechaInicioBoleteria;
        existente.FechaCierreBoleteria = evento.FechaCierreBoleteria;
        // Actualiza categorías según lógica de negocio

        await _context.SaveChangesAsync();
        return existente;
    }

    public async Task<bool> EliminarEventoAsync(int id)
    {
        var evento = await _context.Eventos.FirstOrDefaultAsync(e => e.Id == id && e.Activo);
        if (evento == null) return false;
        evento.Activo = false;
        await _context.SaveChangesAsync();
        return true;
    }
}