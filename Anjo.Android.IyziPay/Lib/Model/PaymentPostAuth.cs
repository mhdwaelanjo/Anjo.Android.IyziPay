using IyziPay.Lib.Request;

namespace IyziPay.Lib.Model
{
    public class PaymentPostAuth : PaymentResource
    {        
        public static PaymentPostAuth Create(CreatePaymentPostAuthRequest request, Options options)
        {
            return RestHttpClient.Create().Post<PaymentPostAuth>(options.BaseUrl + "/payment/postauth", GetHttpHeaders(request, options), request);
        }
    }
}
