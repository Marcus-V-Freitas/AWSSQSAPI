using AWSSQSAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWSSQSAPI.Services.Interfaces
{
    public interface IAWSSQSService
    {
        Task<bool> PostMessageAsync(Usuario usuario);

        Task<List<TodasMensagens>> RecuperarMensagensAsync();

        Task<bool> DeletarMensagensAsync(DeletarMensagem deletarMensagem);
    }
}