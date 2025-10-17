using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.IService
{
    public interface IEventoService
    {
        Task<IEnumerable<EventoDto>> ObtenerEventosAsync();
        Task<EventoDto?> ObtenerEventoPorIdAsync(int id);
        Task<EventoDto> CrearEventoAsync(EventoDto eventoDto);
        Task<EventoDto?> EditarEventoAsync(int id, EventoDto eventoDto);
        Task<bool> EliminarEventoAsync(int id);
    }
}
