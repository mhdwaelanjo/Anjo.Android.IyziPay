using IyziPay.Lib.Model;

namespace IyziPay
{
    public interface IIyziPayPaymentListener
    {
        public void OnIyziPayPaymentSuccess(CheckoutFormInitialize result);
    }
}