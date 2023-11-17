using System;
using IyziPay.Lib.Request;

namespace IyziPay.Lib.Model
{
    public class Disapproval : IyzipayResource
    {
        public String PaymentTransactionId { get; set; }

        public static Disapproval Create(CreateApprovalRequest request, Options options)
        {
            return RestHttpClient.Create().Post<Disapproval>(options.BaseUrl + "/payment/iyzipos/item/disapprove", GetHttpHeaders(request, options), request);
        }
    }
}
