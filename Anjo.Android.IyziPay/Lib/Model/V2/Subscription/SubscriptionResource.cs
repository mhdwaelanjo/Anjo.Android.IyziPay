using System.Collections.Generic;
using Newtonsoft.Json;

namespace IyziPay.Lib.Model.V2.Subscription
{
    public class SubscriptionResource : SubscriptionCreatedResource
    {
        public string CustomerEmail { get; set; }
        
        [JsonProperty(PropertyName = "orders")]
        public List<SubscriptionOrder> SubscriptionOrders { get; set; }
    }
}