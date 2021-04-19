using k8s.Models;

namespace WebhookController
{
    class WebhookCustomResourceDefinition : V1CustomResourceDefinition
    {
        public WebhookCustomResourceDefinition() : 
            base(new WebhookCustomResourceDefinitionSpec(), "Webhook")
        {
        }

        public override void Validate()
        {
            base.Validate();
        }
    }
}
