using System;
using System.Threading.Tasks;

namespace WebhookController
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting K8s Webhook Controller");

            var controller = new Controller();
            await controller.ExecuteAsync(string.Empty);
        }
    }
}
