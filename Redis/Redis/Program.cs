using StackExchange.Redis;
using System;

namespace Redis
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("trezenetredis.redis.cache.windows.net:6380,password=ftXkXFGfHf9Pf4sIJ8LVIgSQTAmU7I38nnRf96qmipE=,ssl=True,abortConnect=False");
            var db = redis.GetDatabase();

            Console.WriteLine("Digite seu RM:");
            var rm = Console.ReadLine();

            Console.WriteLine("Digite seu Nome:");
            var nome = Console.ReadLine();

            db.StringSet(rm, nome);

            Console.WriteLine($"Chave {rm}, valor: {db.StringGet(rm)}");
            Console.Read();
        }
    }
}
