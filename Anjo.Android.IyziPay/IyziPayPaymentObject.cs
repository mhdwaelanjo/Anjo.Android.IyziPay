using IyziPay.Lib.Model;

namespace IyziPay
{
    public class IyziPayPaymentObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string GsmNumber { get; set; }
        public string RegistrationDate { get; set; }
        public string LastLoginDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public string Ip { get; set; }

        public string ApiKey { get; set; }
        public string SecretKey { get; set; }
        public string BaseUrl { get; set; }
        public string CallbackUrl { get; set; }

        public Locale Locale { get; set; }
        public Currency Currency { get; set; }
        public string Price { get; set; }
         
    }
}