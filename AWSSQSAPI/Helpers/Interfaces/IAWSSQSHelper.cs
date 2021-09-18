using Amazon.SQS.Model;
using AWSSQSAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWSSQSAPI.Helpers.Interfaces
{
    public interface IAWSSQSHelper
    {
        Task<bool> SendMessageAsync(DetalheUsuario detalheUsuario);

        Task<List<Message>> ReceiveMessageAsync();

        Task<bool> DeleteMessageAsync(string identificadorEntrega);
    }
}