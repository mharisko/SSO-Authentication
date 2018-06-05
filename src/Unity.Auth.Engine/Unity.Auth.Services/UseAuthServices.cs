using System;
using System.Collections.Generic;
using System.Text;
using Twilio;

namespace Unity.Auth.Services
{
    public static class UseAuthServices
    {
        public static void ConfigureMessageServices(SmsSettings smsSettings, EmailSettings emailSettings)
        {
            if (smsSettings == null)
            {
                throw new ArgumentNullException(nameof(smsSettings));
            }

            if (emailSettings == null)
            {
                throw new ArgumentNullException(nameof(emailSettings));
            }

            TwilioClient.Init(smsSettings.Sid, smsSettings.AuthToken);
        }
    }
}
