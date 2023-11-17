using System.Collections.Generic;

namespace IyziPay.Lib.Model.V2.Transaction
{
    public class TransactionDetailResource : IyzipayResourceV2
    {
        public List<TransactionDetailItem> Payments { get; set; }
    }
}
