using System.Collections.Generic;
using IyziPay.Lib.Request;

namespace IyziPay.Lib.Model
{
    public class InstallmentInfo : IyzipayResource
    {
        public List<InstallmentDetail> InstallmentDetails { get; set; }

        public static InstallmentInfo Retrieve(RetrieveInstallmentInfoRequest request, Options options)
        {
            return RestHttpClient.Create().Post<InstallmentInfo>(options.BaseUrl + "/payment/iyzipos/installment", GetHttpHeaders(request, options), request);
        }
    }
}
