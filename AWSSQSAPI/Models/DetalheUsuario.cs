using System;

namespace AWSSQSAPI.Models
{
    public class DetalheUsuario : Usuario
    {
        public int Id { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AtualizadoEm { get; set; }
    }
}