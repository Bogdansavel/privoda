using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace privoda.Utils.ReCaptchaV2
{
    public class ReCaptchaValidator
    {
        private const string SECRET_KEY = "6Lde1r4UAAAAABDVa2a7y7Pz9G6s7ocw9F4djvif";

        public static ReCaptchaValidationResult IsValid(string captchaResponse)
        {
            if (string.IsNullOrWhiteSpace(captchaResponse))
            {
                return new ReCaptchaValidationResult() { Success = false };
            }

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://www.google.com");

            var values = new List<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>("secret", SECRET_KEY));
            values.Add(new KeyValuePair<string, string>("response", captchaResponse));
            FormUrlEncodedContent content = new FormUrlEncodedContent(values);

            HttpResponseMessage response = client.PostAsync("/recaptcha/api/siteverify", content).Result;

            string verificationResponse = response.Content.ReadAsStringAsync().Result;

            var verificationResult = JsonConvert.DeserializeObject<ReCaptchaValidationResult>(verificationResponse);

            return verificationResult;
        }
    }
}
