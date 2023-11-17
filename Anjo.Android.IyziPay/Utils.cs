using System;
using System.Runtime.CompilerServices;
using Trace = System.Diagnostics.Trace;

namespace IyziPay
{
    internal class Utils
    {
        internal static void DisplayReportResultTrack(Exception exception, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            try
            {
                Trace.WriteLine("ReportMode IyziPay >> message: " + exception.Message + " \n  " + exception.StackTrace);
                Trace.WriteLine("ReportMode IyziPay >> member name: " + memberName);
                Trace.WriteLine("ReportMode IyziPay >> source file path: " + sourceFilePath);
                Trace.WriteLine("ReportMode IyziPay >> source line number: " + sourceLineNumber);
            }
            catch (Exception xx)
            {
                Console.WriteLine(xx);
            }
        }
    }
}