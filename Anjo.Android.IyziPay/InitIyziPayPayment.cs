using Android.App;
using Android.Widget;
using System;
using System.Collections.Generic;
using IyziPay.Lib;
using IyziPay.Lib.Model;
using IyziPay.Lib.Request;

namespace IyziPay
{
    public class InitIyziPayPayment
    {
        private readonly Activity ActivityContext;
        private Dialog IyziPayWindow;
        private IyziPayView HybridView;
        private readonly IyziPayPaymentObject PayPaymentObject;
        public CheckoutFormInitialize CheckoutFormInitialize;
        public IIyziPayPaymentListener Listener;
        internal static InitIyziPayPayment Instance;

        public InitIyziPayPayment(Activity activity, IIyziPayPaymentListener listener, IyziPayPaymentObject options)
        {
            try
            {
                Instance = this;
                ActivityContext = activity;
                PayPaymentObject = options;
                Listener = listener;
                InitIyziPay();
            }
            catch (Exception e)
            {
                Utils.DisplayReportResultTrack(e);
            }
        }

        private bool InitIyziPay()
        {
            try
            {
                if (PayPaymentObject == null)
                    return false;

                CreateCheckoutFormInitializeRequest request = new CreateCheckoutFormInitializeRequest
                {
                    Locale = PayPaymentObject.Locale.ToString(),
                    //request.ConversationId = "123456789";
                    Price = PayPaymentObject.Price,
                    PaidPrice = PayPaymentObject.Price,
                    Currency = PayPaymentObject.Currency.ToString(),
                    //request.CallbackUrl = "https://www.merchant.com/callback";
                    //request.PaymentGroup = PaymentGroup.PRODUCT.ToString();
                    //request.BasketId = "B67832";
                    CallbackUrl = PayPaymentObject.CallbackUrl,
                    EnabledInstallments = new List<int> { 2, 3, 6, 9 },
                };

                Buyer buyer = new Buyer
                {
                    Id = PayPaymentObject.Id,
                    Name = PayPaymentObject.Name,
                    Surname = PayPaymentObject.Surname,
                    GsmNumber = PayPaymentObject.GsmNumber,
                    Email = PayPaymentObject.Email,
                    IdentityNumber = PayPaymentObject.IdentityNumber,
                    //buyer.RegistrationDate = "2013-04-21 15:12:09";
                    //buyer.LastLoginDate = "2015-10-05 12:43:35";
                    RegistrationAddress = PayPaymentObject.Address,
                    //buyer.Ip = "85.34.78.112";
                    City = PayPaymentObject.City,
                    Country = PayPaymentObject.Country,
                    ZipCode = PayPaymentObject.Zip 
                };
                request.Buyer = buyer;

                Address shippingAddress = new Address
                {
                    ContactName = PayPaymentObject.Name + " " + PayPaymentObject.Surname,
                    City = PayPaymentObject.City,
                    Country = PayPaymentObject.Country,
                    Description = PayPaymentObject.Address,
                    ZipCode = PayPaymentObject.Zip
                };
                request.ShippingAddress = shippingAddress;

                Address billingAddress = new Address
                {
                    ContactName = PayPaymentObject.Name + " " + PayPaymentObject.Surname,
                    City = PayPaymentObject.City,
                    Country = PayPaymentObject.Country,
                    Description = PayPaymentObject.Address,
                    ZipCode = PayPaymentObject.Zip
                };
                request.BillingAddress = billingAddress;

                List<BasketItem> basketItems = new List<BasketItem>();
                BasketItem firstBasketItem = new BasketItem
                {
                    Id = "BI1014524",
                    Name = "Top Up Wallet",
                    Category1 = "Top Up Wallet",
                    ItemType = BasketItemType.PHYSICAL.ToString(),
                    Price = PayPaymentObject.Price
                };
                basketItems.Add(firstBasketItem);

                request.BasketItems = basketItems;

                var options = new Options
                {
                    ApiKey = PayPaymentObject.ApiKey,
                    SecretKey = PayPaymentObject.SecretKey
                };

                options.BaseUrl = PayPaymentObject.BaseUrl;
                 
                CheckoutFormInitialize = CheckoutFormInitialize.Create(request, options);

                return true;
            }
            catch (Exception e)
            {
                Utils.DisplayReportResultTrack(e);
                return false;
            }
        }
        
        public void DisplayIyziPayPayment(string price, int theme)
        {
            try
            {
                if (CheckoutFormInitialize == null || string.IsNullOrEmpty(price))
                    return;

                var spendableStringBuilder = new System.Text.StringBuilder();

                spendableStringBuilder.Append("<!DOCTYPE html>");
                spendableStringBuilder.Append("<html>");
                spendableStringBuilder.Append("");
                spendableStringBuilder.Append("<head>");
                spendableStringBuilder.Append("    <meta charset='utf-8'>");
                spendableStringBuilder.Append("    <meta http-equiv='X-UA-Compatible' content='IE=edge'>");
                spendableStringBuilder.Append("    <meta name='viewport' content='width=device-width, initial-scale=1.0'>");
                spendableStringBuilder.Append("    <script src=\"https://ajax.googleapis.com/ajax/libs/jquery/3.1.0/jquery.min.js\"></script>");
                spendableStringBuilder.Append(" ");
                spendableStringBuilder.Append("</head>");
                spendableStringBuilder.Append("");
                spendableStringBuilder.Append("<body>");
                spendableStringBuilder.Append("");
                spendableStringBuilder.Append(CheckoutFormInitialize?.CheckoutFormContent); 
                spendableStringBuilder.Append("");
                spendableStringBuilder.Append("</body>");
                spendableStringBuilder.Append("");
                spendableStringBuilder.Append("</html>");
                
                IyziPayWindow = new Dialog(ActivityContext, theme);
                IyziPayWindow.SetContentView(Resource.Layout.IyziPayWebViewLayout);

                var title = (TextView)IyziPayWindow.FindViewById(Resource.Id.toolbar_title);
                if (title != null)
                    title.Text = ActivityContext.GetText(Resource.String.Lbl_PayWith) + " " + ActivityContext.GetText(Resource.String.Lbl_IyziPay);

                var closeButton = (ImageView)IyziPayWindow.FindViewById(Resource.Id.toolbar_close);
                if (closeButton != null)
                {
                    closeButton.Click += CloseButtonOnClick;
                }

                HybridView = IyziPayWindow.FindViewById<IyziPayView>(Resource.Id.LocalWebView);

                //Set WebView
                if (HybridView != null)
                {
                    HybridView.Initialize(Listener);
                    HybridView.CallbackUrl = PayPaymentObject.CallbackUrl;

                    var dataWebHtml = spendableStringBuilder.ToString();
                    //Load url to be rendered on WebView
                    HybridView.LoadDataWithBaseURL(null, dataWebHtml, "text/html", "UTF-8", null);
                }

                IyziPayWindow.Show(); 
            }
            catch (Exception e)
            {
                Utils.DisplayReportResultTrack(e);
            }
        }

        private void CloseButtonOnClick(object sender, EventArgs e)
        {
            try
            {
                IyziPayWindow.Hide();
                IyziPayWindow.Dismiss();

                Instance = null;
            }
            catch (Exception exception)
            {
                Utils.DisplayReportResultTrack(exception);
            }
        }


        public void StopIyziPay()
        {
            try
            {
                if (IyziPayWindow != null)
                {
                    IyziPayWindow.Hide();
                    IyziPayWindow.Dismiss();
                }

                Instance = null;
            }
            catch (Exception e)
            {
                Utils.DisplayReportResultTrack(e);
            }
        } 
    }
}