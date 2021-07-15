using System;
namespace Wass.Back.Programador.Models.Peticiones.Agenda
{
    public class AgendaRango
    {
        public long idRecurso { get; set; }
        public DateTime fechaInicial { get; set; }
        public DateTime fechaFinal { get; set; }
    }
}
