using IyziPay.Lib.Request;

namespace IyziPay.Lib.Model
{
    public class Apm : ApmResource
    {
        public static Apm Create(CreateApmInitializeRequest request, Options options)
        {
            return RestHttpClient.Create().Post<Apm>(options.BaseUrl + "/payment/apm/initialize", GetHttpHeaders(request, options), request);
        }

        public static Apm Retrieve(RetrieveApmRequest request, Options options)
        {
            return RestHttpClient.Create().Post<Apm>(options.BaseUrl + "/payment/apm/retrieve", GetHttpHeaders(request, options), request);
        }
    }
}
