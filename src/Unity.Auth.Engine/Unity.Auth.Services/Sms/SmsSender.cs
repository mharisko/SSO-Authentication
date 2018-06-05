using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Unity.Auth.Services
{
    public class SmsSender : ISmsSender
    {
        public SmsSender(IOptions<SmsSettings> optionsAccessor)
        {
            this.Options = optionsAccessor.Value;
        }
        public SmsSettings Options { get; }  // set only via Secret Manager

        public async Task SendSmsAsync(string number, string message)
        {
            await MessageResource.CreateAsync(
                   to: new PhoneNumber(number),
                   from: new PhoneNumber(this.Options.From),
                   body: message);
        }

        private async Task HttpClientMethod(string number, string message)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(this.Options.BaseUri) })
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes($"{this.Options.Sid}:{this.Options.AuthToken}")));

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("To",$"+{number}"),
                    new KeyValuePair<string, string>("From", this.Options.From),
                    new KeyValuePair<string, string>("Body", message)
                });

                await client.PostAsync(this.Options.RequestUri, content).ConfigureAwait(false);
            }
        }
    }
}
