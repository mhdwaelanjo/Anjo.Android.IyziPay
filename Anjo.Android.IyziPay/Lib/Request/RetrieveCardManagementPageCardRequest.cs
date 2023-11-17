using System;

namespace IyziPay.Lib.Request
{
    public class RetrieveCardManagementPageCardRequest : BaseRequest
    {
        public string PageToken { get; set; }
        
        public override String ToPKIRequestString()
        {
            return ToStringRequestBuilder.NewInstance()
                .AppendSuper(base.ToPKIRequestString())
                .Append("token", PageToken)
                .GetRequestString();
        }
    }
}