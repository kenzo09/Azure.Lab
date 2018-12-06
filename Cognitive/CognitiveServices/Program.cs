using System;
using System.Collections.Generic;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Newtonsoft.Json;

namespace CognitiveServices
{
    class Program
    {
        static void Main(string[] args)
        {
            var subKey = "002d9cf2be714679abeedb67f0bf137a";
            var imgUrl = "https://mundoeducacao.bol.uol.com.br/upload/conteudo_legenda/016f2c4fff481982f07192e812360966.jpg";
            var endPoint = "https://brazilsouth.api.cognitive.microsoft.com/";

            ComputerVisionClient client = new ComputerVisionClient(
                new ApiKeyServiceClientCredentials(subKey));

            client.Endpoint = endPoint;

            var features = new List<VisualFeatureTypes>
            {
                VisualFeatureTypes.Faces,
                VisualFeatureTypes.Tags,
                VisualFeatureTypes.Categories,
                VisualFeatureTypes.Description
            };

            var res = client.AnalyzeImageAsync(imgUrl, features).Result;

            Console.WriteLine(JsonConvert.SerializeObject(res));
            Console.Read();
        }
    }
}
