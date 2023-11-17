using System.Collections.Generic;
using IyziPay.Lib.Request;

namespace IyziPay.Lib.Model
{
    public class PayoutCompletedTransactionList : IyzipayResource
    {
        public List<PayoutCompletedTransaction> PayoutCompletedTransactions { get; set; }

        public static PayoutCompletedTransactionList Retrieve(RetrieveTransactionsRequest request, Options options)
        {
            return RestHttpClient.Create().Post<PayoutCompletedTransactionList>(options.BaseUrl + "/reporting/settlement/payoutcompleted", GetHttpHeaders(request, options), request);
        }
    }
}
