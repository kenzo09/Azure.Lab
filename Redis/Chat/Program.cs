using StackExchange.Redis;
using System;

namespace Chat
{
    class Program
    {
        private const string Channel = "13net";
        static void Main(string[] args)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("trezenetredis.redis.cache.windows.net:6380,password=ftXkXFGfHf9Pf4sIJ8LVIgSQTAmU7I38nnRf96qmipE=,ssl=True,abortConnect=False");
            var db = redis.GetDatabase();

            Console.WriteLine("Digite seu Nome:");
            var nome = Console.ReadLine();

            var pubsub = redis.GetSubscriber();
            pubsub.Subscribe(Channel, (_, msg) => Console.WriteLine(msg.ToString()));

            db.Publish(Channel, $"{nome} entrou na sala");

            while (true)
            {
                db.Publish(Channel, Console.ReadLine());
            }
        }
    }
}
