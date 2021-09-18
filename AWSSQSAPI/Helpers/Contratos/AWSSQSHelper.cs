using Amazon.SQS;
using Amazon.SQS.Model;
using AWSSQSAPI.Helpers.Interfaces;
using AWSSQSAPI.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AWSSQSAPI.Helpers.Contratos
{
    public class AWSSQSHelper : IAWSSQSHelper
    {
        private readonly IAmazonSQS _sqs;
        private readonly ServiceConfiguration _service;

        public AWSSQSHelper(IAmazonSQS sqs, IOptions<ServiceConfiguration> service)
        {
            _sqs = sqs;
            _service = service.Value;
        }

        public async Task<bool> DeleteMessageAsync(string identificadorEntrega)
        {
            try
            {
                var deleteResult = await _sqs.DeleteMessageAsync(_service.AWSSQS.QueueURL, identificadorEntrega);
                return deleteResult.HttpStatusCode == HttpStatusCode.OK;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Message>> ReceiveMessageAsync()
        {
            try
            {
                var request = new ReceiveMessageRequest()
                {
                    QueueUrl = _service.AWSSQS.QueueURL,
                    MaxNumberOfMessages = 10,
                    WaitTimeSeconds = 5
                };
                var result = await _sqs.ReceiveMessageAsync(request);
                return result.Messages.Any() ? result.Messages : new();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> SendMessageAsync(DetalheUsuario detalheUsuario)
        {
            try
            {
                string mensagem = JsonConvert.SerializeObject(detalheUsuario);
                var sendRequest = new SendMessageRequest(_service.AWSSQS.QueueURL, mensagem);
                var sendResult = await _sqs.SendMessageAsync(sendRequest);
                return sendResult.HttpStatusCode == HttpStatusCode.OK;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}