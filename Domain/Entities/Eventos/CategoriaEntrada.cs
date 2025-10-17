using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Eventos
{
    public class CategoriaEntrada
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public int EventoId { get; set; }
        public Evento Evento { get; set; } = null!;
    }
}
