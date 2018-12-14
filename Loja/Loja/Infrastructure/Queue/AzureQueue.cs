using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loja.Core.Entities;
using Loja.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace Loja.Infrastructure.Queue
{
    public class AzureQueue
    {
        public AzureQueue(IConfiguration config)
        {
            _account = CloudStorageAccount.Parse(config.GetSection("Azure:Storage").Value);
            _queueClient = _account.CreateCloudQueueClient();
        }

        private readonly CloudStorageAccount _account;
        private readonly CloudQueueClient _queueClient;

        public void AddProduto(Produto produto)
        {
            var json = JsonConvert.SerializeObject(produto);

            var produtosQueue = _queueClient.GetQueueReference("produtos");
            produtosQueue.CreateIfNotExistsAsync().Wait();

            var message = new CloudQueueMessage(json);
            produtosQueue.AddMessageAsync(message).Wait();
        }
    }
}
