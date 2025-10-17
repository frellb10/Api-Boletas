using Domain.Entities.Eventos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IEventoRepository
    {
        Task<IEnumerable<Evento>> ObtenerEventosAsync();
        Task<Evento?> ObtenerEventoPorIdAsync(int id);
        Task<Evento> CrearEventoAsync(Evento evento);
        Task<Evento?> EditarEventoAsync(Evento evento);
        Task<bool> EliminarEventoAsync(int id);
    }
}