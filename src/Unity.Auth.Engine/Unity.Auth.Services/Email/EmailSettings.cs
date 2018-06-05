using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Unity.Auth.Services
{
    public class EmailSettings
    {
        public string ApiKey { get; internal set; }
        public string FromAddress { get; internal set; }
        public string FromName { get; internal set; }
    }
}
