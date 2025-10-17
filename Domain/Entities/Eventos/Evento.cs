using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Eventos
{
    public class Evento
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Lugar { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string? ImagenUrl { get; set; }
        public int AforoMaximo { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaInicioBoleteria { get; set; }
        public DateTime FechaCierreBoleteria { get; set; }
        public ICollection<CategoriaEntrada> CategoriasEntrada { get; set; } = new List<CategoriaEntrada>();
    }
}
