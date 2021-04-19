using k8s.Models;
using System.Collections.Generic;

namespace WebhookController
{
    class WebhookCustomResourceDefinitionSpec : V1CustomResourceDefinitionSpec
    {
        public WebhookCustomResourceDefinitionSpec()
            : base("jannemattila.com",
                  new V1CustomResourceDefinitionNames(
                      kind: "Webhook",
                      singular: "webhook",
                      plural: "webhooks",
                      shortNames: new List<string>
                      { 
                          "wh"
                      }), 
                  scope: "Namespaced",
                  versions: new List<V1CustomResourceDefinitionVersion>()
                  {
                      new WebhookCustomResourceDefinitionVersion()
                  })
        {
        }
    }
}
