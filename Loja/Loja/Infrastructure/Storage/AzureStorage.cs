using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loja.Core.Entities;
using Loja.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace Loja.Infrastructure.Storage
{
    public class AzureStorage : IAzureStorage
    {
        public AzureStorage(IConfiguration config)
        {
            _account = CloudStorageAccount.Parse(config.GetSection("Azure:Storage").Value);
            _tableClient = _account.CreateCloudTableClient();
        }

        private const string PatitionKey = "13net";
        private readonly CloudStorageAccount _account;
        private readonly CloudTableClient _tableClient;

        public void AddProduto(Produto produto)
        {
            var json = JsonConvert.SerializeObject(produto);

            var table = _tableClient.GetTableReference("produtos");
            table.CreateIfNotExistsAsync().Wait();

            var entity = new ProdutoEntity(PatitionKey, produto.Id.ToString())
            {
                Produto = json
            };

            TableOperation operation = TableOperation.Insert(entity);
            table.ExecuteAsync(operation).Wait();
        }

        public async Task<List<Produto>> ObterProdutos()
        {
            var table = _tableClient.GetTableReference("produtos");
            table.CreateIfNotExistsAsync().Wait();

            var query = new TableQuery<ProdutoEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, PatitionKey));

            TableContinuationToken token = null;

            var segment = await table.ExecuteQuerySegmentedAsync(query, token);
            var produtosEntity = segment.ToList();


            return produtosEntity
                .Where(x => x.Produto != null)
                .Select(x => JsonConvert.DeserializeObject<Produto>(x.Produto)
            ).ToList();
        }

        public async Task<Produto> ObterProduto(int id)
        {
            var table = _tableClient.GetTableReference("produtos");
            table.CreateIfNotExistsAsync().Wait();

            var query = new TableQuery<ProdutoEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, PatitionKey))
                .Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id.ToString()));

            TableContinuationToken token = null;

            var segment = await table.ExecuteQuerySegmentedAsync(query, token);
            var produtoEntity = segment.FirstOrDefault();

            return JsonConvert.DeserializeObject<Produto>(produtoEntity.Produto);
        }
    }
}
