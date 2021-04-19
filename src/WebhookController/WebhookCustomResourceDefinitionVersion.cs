using k8s.Models;

namespace WebhookController
{
    class WebhookCustomResourceDefinitionVersion : V1CustomResourceDefinitionVersion
    {
        public WebhookCustomResourceDefinitionVersion()
            : base(name: "v1alpha1", served: true, storage: true,
                  schema: new WebhookCustomResourceSchema())
        {
        }
    }
}
