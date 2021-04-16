using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebhookController
{
    class Controller
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new();

        public Controller()
        {
            Console.CancelKeyPress += (sender, a) =>
            {
                a.Cancel = true;
                Console.WriteLine("Closing down...");
                _cancellationTokenSource.Cancel();
            };
        }

        public async Task ExecuteAsync(string webhook)
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                await Task.Delay(1000, _cancellationTokenSource.Token);
            }
        }
    }
}
