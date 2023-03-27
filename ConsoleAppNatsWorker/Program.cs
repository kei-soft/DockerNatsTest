using NATS.Client;

namespace ConsoleAppNatsWorker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Worker");

            ConnectionFactory cf = new ConnectionFactory();
            Options opts = ConnectionFactory.GetDefaultOptions();
            opts.Url = "nats://localhost:4222";

            IConnection c = cf.CreateConnection(opts);

            EventHandler<MsgHandlerEventArgs> h = (sender, args) =>
            {
                Console.WriteLine($"worker received {args.Message}");
            };

            IAsyncSubscription s = c.SubscribeAsync("worker", h);

            while (true)
            {
                Console.WriteLine("worker listening...");
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
        }
    }
}