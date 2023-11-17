using IyziPay.Lib.Request;

namespace IyziPay.Lib.Model
{
    public class CrossBookingFromSubMerchant : IyzipayResource
    {
        public static CrossBookingFromSubMerchant Create(CreateCrossBookingRequest request, Options options)
        {
            return RestHttpClient.Create().Post<CrossBookingFromSubMerchant>(options.BaseUrl + "/crossbooking/receive", GetHttpHeaders(request, options), request);
        }
    }
}
