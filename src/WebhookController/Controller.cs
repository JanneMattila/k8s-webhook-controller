using k8s;
using System;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace WebhookController
{
    class Controller
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new();
        private readonly HttpClient _client = new();
        private readonly Kubernetes _kubernetes = new(KubernetesClientConfiguration.BuildConfigFromConfigFile());

        public Controller()
        {
            Console.CancelKeyPress += (sender, a) =>
            {
                a.Cancel = true;
                Console.WriteLine("Closing down...");
                _cancellationTokenSource.Cancel();
            };
        }

        private async void OnEvent(WatchEventType eventType, WebhookCustomResourceDefinition resource)
        {
            var json = JsonSerializer.Serialize(resource);
            using var content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
            await _client.PostAsync($"?invoke={eventType}", content);
        }

        private async void OnError(Exception exception)
        {
            var json = JsonSerializer.Serialize(exception);
            using var content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
            await _client.PostAsync($"?invoke=Exception", content);
        }

        public async Task ExecuteAsync(string webhook)
        {
            var watcher = await _kubernetes.WatchObjectAsync<WebhookCustomResourceDefinition>(
                path: Contants.ApiPath,
                onEvent: (eventType, resource) => OnEvent(eventType, resource),
                onError: e => OnError(e),
                cancellationToken: _cancellationTokenSource.Token);
            await Task.Delay(-1, _cancellationTokenSource.Token);
        }
    }
}
