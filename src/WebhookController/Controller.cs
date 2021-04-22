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
        private readonly HttpClient _client;
        private readonly CancellationTokenSource _cancellationTokenSource = new();
        private readonly Kubernetes _kubernetes = new(KubernetesClientConfiguration.BuildConfigFromConfigFile());

        public Controller(string webhook)
        {
            Console.CancelKeyPress += (sender, a) =>
            {
                a.Cancel = true;
                Console.WriteLine("Closing down...");
                _cancellationTokenSource.Cancel();
            };

            _client = new HttpClient()
            {
                BaseAddress = new Uri(webhook)
            };
        }

        private async void OnEvent(WatchEventType eventType, WebhookCustomResourceDefinition resource)
        {
            var json = JsonSerializer.Serialize(resource);
            Console.WriteLine($"{eventType}: {json}");
            using var content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
            await _client.PostAsync($"?invoke={eventType}", content);
        }

        private async void OnError(Exception exception)
        {
            Console.WriteLine($"Exception: {exception}");
            var json = JsonSerializer.Serialize(exception);
            using var content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
            await _client.PostAsync($"?invoke=Exception", content);
        }

        public async Task ExecuteAsync()
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
