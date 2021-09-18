using AWSSQSAPI.Helpers.Interfaces;
using AWSSQSAPI.Models;
using AWSSQSAPI.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSSQSAPI.Services.Contratos
{
    public class AWSSQSService : IAWSSQSService
    {
        private readonly IAWSSQSHelper _helper;

        public AWSSQSService(IAWSSQSHelper helper)
        {
            _helper = helper;
        }

        public async Task<bool> DeletarMensagensAsync(DeletarMensagem deletarMensagem)
        {
            try
            {
                return await _helper.DeleteMessageAsync(deletarMensagem.IdentificadorEntrega);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> PostMessageAsync(Usuario usuario)
        {
            try
            {
                DetalheUsuario detalheUsuario = new()
                {
                    Id = new Random().Next(int.MaxValue),
                    PrimeiroNome = usuario.PrimeiroNome,
                    UltimoNome = usuario.UltimoNome,
                    EmailId = usuario.EmailId,
                    UsuarioNome = usuario.UsuarioNome,
                    CriadoEm = DateTime.UtcNow,
                    AtualizadoEm = DateTime.UtcNow,
                };
                return await _helper.SendMessageAsync(detalheUsuario);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<TodasMensagens>> RecuperarMensagensAsync()
        {
            var todasMensagens = new List<TodasMensagens>();
            try
            {
                var mensagens = await _helper.ReceiveMessageAsync();
                todasMensagens = mensagens.Select(x => new TodasMensagens()
                {
                    MensagemId = x.MessageId,
                    IdentificadorEntrega = x.ReceiptHandle,
                    DetalheUsuario = JsonConvert.DeserializeObject<DetalheUsuario>(x.Body)
                }).ToList();

                return todasMensagens;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}