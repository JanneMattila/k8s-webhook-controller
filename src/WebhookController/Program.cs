using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace WebhookController
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting K8s Webhook Controller");

            var builder = new ConfigurationBuilder()
#if DEBUG
                .AddUserSecrets<Program>()
#endif
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            var webhook = configuration.GetValue<string>("webhook");

            var controller = new Controller(webhook);
            await controller.ExecuteAsync();
        }
    }
}
