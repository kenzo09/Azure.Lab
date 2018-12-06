using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AzureSearch
{
    class IndexLetras
    {
        [Key]
        [IsSortable]
        [IsFilterable]
        [IsRetrievable(true)]
        public string Id { get; set; }

        [IsSortable]
        [IsFilterable]
        [IsRetrievable(true)]
        public string NomeBanda { get; set; }

        [IsSortable]
        [IsFilterable]
        [IsRetrievable(true)]
        public string Album { get; set; }

        [IsSortable]
        [IsFilterable]
        [IsSearchable]
        [IsRetrievable(true)]
        public string Letra { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SearchServiceClient searchServiceClient =
                new SearchServiceClient("teste-azuresearch-dois",
                new SearchCredentials("4A91F9970AB73ADB4106D6BC56FD0EE2"));

            var index = searchServiceClient.Indexes.GetClient("index-bandas");

            // Custom index
            var index2 = searchServiceClient.Indexes.Get("index-bandas");

            index2.Analyzers.Add(new PatternAnalyzer
            {
                Name = "token",
                Pattern = @"<!!!>"
            });

            index2.Analyzers.Add(new CustomAnalyzer
            {
                Name = "custom",
                Tokenizer = TokenizerName.Standard,
                TokenFilters = new[]
                {
                    TokenFilterName.Phonetic,
                    TokenFilterName.Lowercase,
                    TokenFilterName.AsciiFolding
                }
            });

            index2.Fields[3].Analyzer = "custom";
            searchServiceClient.Indexes.CreateOrUpdate(index2, true);

            var batch = IndexBatch.Upload(new List<IndexLetras>
            {
                new IndexLetras
                {
                    Id = "rm331556",
                    Album = "O Sol",
                    NomeBanda = "Vitor Kley",
                    #region Letra
                    Letra = @"Ô, Sol
Vê se não esquece e me ilumina
Preciso de você aqui
Ô, Sol
Vê se enriquece a minha melanina
Só você me faz sorrir

E quando você vem
Tudo fica bem mais tranquilo
Ô, tranquilo
Que assim seja, amém
O seu brilho é o meu abrigo
Meu abrigo

E toda vez que você sai
O mundo se distrai
Quem ficar, ficou
Quem foi, vai, vai
Toda vez que você sai
O mundo se distrai
Quem ficar, ficou
Quem foi, vai, vai, vai
Quem foi, vai, vai, vai
Quem foi

Ô, Sol
Vê se não esquece e me ilumina
Preciso de você aqui
Ô, Sol
Vê se enriquece a minha melanina
Só você me faz sorrir

E quando você vem
Tudo fica bem mais tranquilo
Ô, tranquilo
Que assim seja, amém
O seu brilho é o meu abrigo
Meu abrigo

E toda vez que você sai
O mundo se distrai
Quem ficar, ficou
Quem foi, vai, vai
Toda vez que você sai
O mundo se distrai
Quem ficar, ficou
Quem foi, vai, vai, vai
Quem foi, vai, vai, vai
Quem foi, vai, vai

Ô, Sol
Vem, aquece a minha alma
E mantém a minha calma
Não esquece que eu existo
E me faz ficar tranquilo
(Sol)
Vem, aquece a minha alma
E mantém a minha calma
Não esquece que eu existo
E me faz ficar tranquilo

E toda vez que você sai
O mundo se distrai
Quem ficar, ficou
Quem foi, vai vai
Toda vez que você sai
O mundo se distrai
Quem ficar, ficou
Quem foi, vai, vai, vai
Quem foi, vai, vai, vai
Quem foi, vai, vai, vai"
#endregion
                }
            });

            //index.Documents.Index(batch);

            Console.WriteLine("Digite o termo para a busca:");
            var termo = Console.ReadLine();
            var result = index.Documents.Search<IndexLetras>(termo, new SearchParameters { IncludeTotalResultCount = true });

            Console.WriteLine($"{result.Count} resultados encontrados");
            foreach (var doc in result.Results)
            {
                Console.WriteLine($"{doc.Document.Id} - {doc.Document.NomeBanda}");
            }

            Console.WriteLine("Deu");
            Console.ReadLine();
        }
    }
}
