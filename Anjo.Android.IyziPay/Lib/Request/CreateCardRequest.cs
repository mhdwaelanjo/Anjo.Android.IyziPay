using System;
using IyziPay.Lib.Model;

namespace IyziPay.Lib.Request
{
    public class CreateCardRequest : BaseRequest
    {
        public String ExternalId { get; set; }
        public String Email { get; set; }
        public String CardUserKey { get; set; }
        public CardInformation Card { get; set; }

        public override String ToPKIRequestString()
        {
            return ToStringRequestBuilder.NewInstance()
                .AppendSuper(base.ToPKIRequestString())
                .Append("externalId", ExternalId)
                .Append("email", Email)
                .Append("cardUserKey", CardUserKey)
                .Append("card", Card)
                .GetRequestString();
        }
    }
}
