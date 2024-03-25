using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace ggsLauncher
{
    public class HybridUtils
    {
        public static string GetDevicecodetoken()
        {
            RestClient restClient = new RestClient("https://account-public-service-prod03.ol.epicgames.com/account/api/oauth/token");
            RestRequest restRequest = new RestRequest(Method.POST);
            restRequest.AddHeader("Authorization", "Basic OThmN2U0MmMyZTNhNGY4NmE3NGViNDNmYmI0MWVkMzk6MGEyNDQ5YTItMDAxYS00NTFlLWFmZWMtM2U4MTI5MDFjNGQ3");
            restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            restRequest.AddParameter("grant_type", "client_credentials");
            RestRequest restRequest2 = restRequest;
            string[] array = restClient.Execute(restRequest2).Content.Split(new char[]
            {
                ':'
            }, 26);
            string result;
            try
            {
                result = array[1].ToString().Split(new char[]
                {
                    ','
                }, 2)[0].ToString().Split(new char[]
                {
                    '"'
                }, 2)[1].ToString().Split(new char[]
                {
                    '"'
                }, 2)[0].ToString();
            }
            catch
            {
                Process.GetCurrentProcess().Kill();
                result = "error";
            }
            return result;
        }

        public static string GetDevicecode(string auth)
        {
            RestClient restClient = new RestClient("https://account-public-service-prod03.ol.epicgames.com/account/api/oauth/deviceAuthorization");
            RestRequest restRequest = new RestRequest(Method.POST);
            restRequest.AddHeader("Authorization", "Bearer " + auth);
            restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            RestRequest restRequest2 = restRequest;
            string[] array = restClient.Execute(restRequest2).Content.Split(new char[]
            {
                ','
            }, 8);
            string[] array2 = array[3].ToString().Split(new char[]
            {
                '"'
            }, 4)[3].ToString().Split(new char[]
            {
                '"'
            }, 2);
            string[] array3 = array[1].ToString().Split(new char[]
            {
                '"'
            }, 4)[3].ToString().Split(new char[]
            {
                '"'
            }, 2);
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = array2[0],
                UseShellExecute = true
            };
            Process.Start(startInfo);
            string content;
            for (; ; )
            {
                RestClient restClient2 = new RestClient("https://account-public-service-prod03.ol.epicgames.com/account/api/oauth/token");
                RestRequest restRequest3 = new RestRequest(Method.POST);
                restRequest3.AddHeader("Authorization", "Basic OThmN2U0MmMyZTNhNGY4NmE3NGViNDNmYmI0MWVkMzk6MGEyNDQ5YTItMDAxYS00NTFlLWFmZWMtM2U4MTI5MDFjNGQ3");
                restRequest3.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                restRequest3.AddParameter("grant_type", "device_code");
                restRequest3.AddParameter("device_code", array3[0].ToString());
                RestRequest restRequest4 = restRequest3;
                content = restClient2.Execute(restRequest4).Content;
                if (content.Contains("access_token"))
                {
                    break;
                }
                content.Contains("errors.com.epicgames.not_found");
                Thread.Sleep(150);
            }
            string[] array4 = content.Split(new char[]
            {
                ':'
            }, 26);
            return array4[1].ToString().Split(new char[]
            {
                ','
            }, 2)[0].ToString().Split(new char[]
            {
                '"'
            }, 2)[1].ToString().Split(new char[]
            {
                '"'
            }, 2)[0].ToString() + "," + array4[16].ToString().Split(new char[]
            {
                ','
            }, 2)[0];
        }

        public static string GetToken(string authCode)
        {
            RestClient restClient = new RestClient("https://account-public-service-prod.ol.epicgames.com/account/api/oauth/token");
            RestRequest restRequest = new RestRequest(Method.POST);
            restRequest.AddHeader("Authorization", "basic OThmN2U0MmMyZTNhNGY4NmE3NGViNDNmYmI0MWVkMzk6MGEyNDQ5YTItMDAxYS00NTFlLWFmZWMtM2U4MTI5MDFjNGQ3");
            restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            restRequest.AddParameter("grant_type", "authorization_code");
            restRequest.AddParameter("code", authCode);
            RestRequest restRequest2 = restRequest;
            string content = restClient.Execute(restRequest2).Content;
            if (content.Contains("access_token"))
            {
                string[] array = content.Split(new char[]
                {
                    ':'
                }, 26);
                string str = array[17].ToString().Split(new char[]
                {
                    ','
                }, 2)[0];
                return array[1].ToString().Split(new char[]
                {
                    ','
                }, 2)[0].ToString().Split(new char[]
                {
                    '"'
                }, 2)[1].ToString().Split(new char[]
                {
                    '"'
                }, 2)[0].ToString() + "," + str;
            }
            if (content.Contains("It is possible that it was no longer valid"))
            {
                Process.Start("https://www.epicgames.com/id/logout?redirectUrl=https%3A//www.epicgames.com/id/login%3FredirectUrl%3Dhttps%253A%252F%252Fwww.epicgames.com%252Fid%252Fapi%252Fredirect%253FclientId%253D3446cd72694c4a4485d81b77adbb2141%2526responseType%253Dcode");
                return "error";
            }
            return "error";
        }

        public static string GetExchange(string token)
        {
            RestClient restClient = new RestClient("https://account-public-service-prod.ol.epicgames.com/account/api/oauth/exchange");
            RestRequest restRequest = new RestRequest(0);
            restRequest.AddHeader("Authorization", "bearer " + token);
            RestRequest restRequest2 = restRequest;
            string content = restClient.Execute(restRequest2).Content;
            if (content.Contains("errors.com.epicgames.common.oauth.invalid_token"))
            {
                return "error";
            }
            return content.Split(new char[]
            {
                ','
            }, 4)[1].ToString().Split(new char[]
            {
                ','
            }, 2)[0].ToString().Split(new char[]
            {
                '"'
            }, 2)[1].ToString().Split(new char[]
            {
                '"'
            }, 2)[1].ToString().Split(new char[]
            {
                '"'
            }, 2)[1].ToString().Split(new char[]
            {
                '"'
            }, 2)[0].ToString();
        }
    }

    public class DeviceCode
    {
        [JsonPropertyName("user_code")]
        public int user_code { get; set; }

        [JsonPropertyName("device_code")]
        public string device_code { get; set; }

        [JsonPropertyName("verification_uri")]
        public string verification_uri { get; set; }

        [JsonPropertyName("verification_uri_complete")]
        public string verification_uri_complete { get; set; }

        [JsonPropertyName("prompt")]
        public string prompt { get; set; }

        [JsonPropertyName("expires_in")]
        public string expires_in { get; set; }

        [JsonPropertyName("interval")]
        public string interval { get; set; }

        [JsonPropertyName("client_id")]
        public string client_id { get; set; }
    }

    public class ExchangeCode
    {
        [JsonPropertyName("expiresInSeconds")]
        public int ExpiresInSeconds { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("creatingClientId")]
        public string CreatingClientId { get; set; }
    }
}
