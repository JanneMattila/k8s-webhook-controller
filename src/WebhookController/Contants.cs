namespace WebhookController
{
    static class Contants
    {
        public static readonly string Group = "jannemattila.com";
        public static readonly string Version = "v1alpha1";
        public static readonly string Kind = "Webhook";
        public static readonly string Signular = "webhook";
        public static readonly string Plural = "webhooks";

        public static readonly string ApiPath = $"apis/{Group}/{Version}/watch/{Plural}";
    }
}
