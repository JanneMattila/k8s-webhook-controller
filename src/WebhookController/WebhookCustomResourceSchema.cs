using k8s.Models;
using System.Collections.Generic;

namespace WebhookController
{
    class WebhookCustomResourceSchema : V1CustomResourceValidation
    {
        public WebhookCustomResourceSchema()
            : base(new V1JSONSchemaProps(
                definitions: new Dictionary<string, V1JSONSchemaProps>
                {
                    {
                        "spec", new V1JSONSchemaProps(
                            type: "object",
                            definitions: new Dictionary<string, V1JSONSchemaProps>
                            {
                                { "uri", new V1JSONSchemaProps(type: "string") }
                            })
                    }
                }))
        {
        }
    }
}
