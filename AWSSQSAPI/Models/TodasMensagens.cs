namespace AWSSQSAPI.Models
{
    public class TodasMensagens
    {
        public string MensagemId { get; set; }
        public string IdentificadorEntrega { get; set; }

        public DetalheUsuario DetalheUsuario { get; set; }

        public TodasMensagens()
        {
            DetalheUsuario = new();
        }
    }
}