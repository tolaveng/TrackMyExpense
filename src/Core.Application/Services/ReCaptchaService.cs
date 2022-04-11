using Core.Application.Services.IServices;
using Core.Application.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class ReCaptchaService : IReCaptchaService
    {
        private readonly ReCaptchaSetting _reCaptchaSetting;

        public ReCaptchaService(IOptions<ReCaptchaSetting> reCaptchaSetting)
        {
            _reCaptchaSetting = reCaptchaSetting.Value;
        }

        public async Task<bool> ValidateCaptcha(string code)
        {
            var result = false;
            var googleVerificationUrl = "https://www.google.com/recaptcha/api/siteverify";
            try
            {
                using var client = new HttpClient();
                var response = await client.PostAsync($"{googleVerificationUrl}?secret={_reCaptchaSetting.SecretKey}&response={code}", null);
                var jsonString = await response.Content.ReadAsStringAsync();
                var captchaVerfication = JsonConvert.DeserializeObject<CaptchaResponse>(jsonString);
                if (captchaVerfication != null)
                {
                    result = captchaVerfication.success;
                }
            }
            catch (Exception)
            {
                //ignored
            }
            return result;
        }

        private class CaptchaResponse
        {
            public bool success { get; set; }
            public string challenge_ts { get; set; } = string.Empty;
            public string hostname { get; set; } = string.Empty;
        }
    }
}
