using System;
using System.Diagnostics.CodeAnalysis;
using Android.Annotation;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Webkit;

namespace IyziPay
{
    internal class IyziPayView : WebView
    {
        protected IyziPayView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public IyziPayView([NotNull] Context context) : base(context)
        {
            InitWebView();
        }

        public IyziPayView([NotNull] Context context, IAttributeSet attrs) : base(context, attrs)
        {
            InitWebView();
        }

        public IyziPayView([NotNull] Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            InitWebView();
        }

        [Obsolete]
        public IyziPayView([NotNull] Context context, IAttributeSet attrs, int defStyleAttr, bool privateBrowsing) : base(context, attrs, defStyleAttr, privateBrowsing)
        {
            InitWebView();
        }

        public IyziPayView([NotNull] Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            InitWebView();
        }

        private IIyziPayPaymentListener IyziPayPaymentListener;
        public void Initialize(IIyziPayPaymentListener listener)
        {
            try
            {
                IyziPayPaymentListener = listener;
                InitWebView();
            }
            catch (Exception e)
            {
                Utils.DisplayReportResultTrack(e);
            }
        }

        [SuppressLint(Value = new[] { "SetJavaScriptEnabled" })]
        private void InitWebView()
        {
            try
            {
                WebSettings settings = Settings;
                settings.JavaScriptEnabled = true;
                settings.CacheMode = CacheModes.NoCache;
                settings.MediaPlaybackRequiresUserGesture = false;

                settings.LoadsImagesAutomatically = true; 
                settings.JavaScriptCanOpenWindowsAutomatically = true;
                settings.SetLayoutAlgorithm(WebSettings.LayoutAlgorithm.TextAutosizing);
                settings.DomStorageEnabled = true;
                settings.AllowFileAccess = true;
                settings.DefaultTextEncodingName = "utf-8";

                string newUA = "Mozilla/5.0 (X11; U; Linux i686; en-US; rv:1.9.0.4) Gecko/20100101 Firefox/4.0";
                settings.UserAgentString = newUA;

                settings.UseWideViewPort = true;
                settings.LoadWithOverviewMode = true;

                settings.SetSupportZoom(false);
                settings.BuiltInZoomControls = false;
                settings.DisplayZoomControls = false;

                SetWebChromeClient(new MyWebChromeClient(this));
                SetWebViewClient(new MyWebViewClient(this));
            }
            catch (Exception e)
            {
                Utils.DisplayReportResultTrack(e);
            }
        }

        public string CallbackUrl { get; set; }


        private class MyWebViewClient : WebViewClient 
        {
            private IyziPayView IyziPayView;
            public MyWebViewClient(IyziPayView iyziPayView)
            {
                IyziPayView = iyziPayView;
            }

            public override bool ShouldOverrideUrlLoading(WebView view, IWebResourceRequest request)
            {
                try
                {
                    if (string.IsNullOrEmpty(request?.Url?.ToString()))
                        return false;

                    if (request.Url.ToString().Contains("&mobile=true"))
                    {
                        view.LoadUrl(request.Url.ToString().Replace("&mobile=true", "&mobile=false"));
                    } 
                }
                catch (Exception e)
                {
                    Utils.DisplayReportResultTrack(e);
                }
                return false;
            }

            //public override void OnPageStarted(WebView view, string url, Bitmap favicon)
            //{
            //    try
            //    {
            //        if (url.Contains("requests.php?f=iyzipay"))
            //        {
            //            //make something
            //            if (InitIyziPayPayment.Instance != null)
            //                InitIyziPayPayment.Instance.Listener?.OnIyziPayPaymentSuccess(InitIyziPayPayment.Instance.CheckoutFormInitialize);
            //        }
                    
            //        base.OnPageStarted(view, url, favicon);
            //    }
            //    catch (Exception e)
            //    {
            //        Utils.DisplayReportResultTrack(e);
            //    }
            //}
        } 

        private class MyWebChromeClient : WebChromeClient
        {
            private IyziPayView IyziPayView;
            public MyWebChromeClient(IyziPayView iyziPayView)
            {
                IyziPayView = iyziPayView;
            }

            public override void OnReceivedTitle(WebView view, string title)
            {
                base.OnReceivedTitle(view, title);
                if (view.Url.Contains(IyziPayView.CallbackUrl))
                {
                    //make something
                    if (InitIyziPayPayment.Instance != null)
                        InitIyziPayPayment.Instance.Listener?.OnIyziPayPaymentSuccess(InitIyziPayPayment.Instance.CheckoutFormInitialize);
                }
            }
        } 
    }
}