using System.Runtime.Serialization;

namespace Unity.Auth.Services
{
    public class SmsSettings
    {
        public string Sid { get; set; }
        public string AuthToken { get; set; }
        public string From { get; set; }
        public string RequestUri { get; set; }
        public string BaseUri { get; set; }
    }
}