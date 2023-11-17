using Newtonsoft.Json;

namespace IyziPay.Lib.Request.V2.Subscription
{
    public class RetrySubscriptionRequest : BaseRequestV2
    {
        [JsonProperty(PropertyName = "referenceCode")]
        public string SubscriptionOrderReferenceCode { get; set; }
    }
}