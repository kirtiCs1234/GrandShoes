using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace Helper
{
	public class ServerResponse
	{
		public static T Invoke<T>(string Url, string Body, string Method)
		{
			string apiUrl = "";
			if (HttpContext.Current.Request.IsLocal)
			{
				apiUrl = "http://localhost:54378/";
				//apiUrl = "http://erpmain-001-site11.ftempurl.com/";
			}
			else
			{
				apiUrl = "http://erpmain-001-site11.ftempurl.com/";
			}
			//string apiUrl = (HttpContext.Current.Request.IsLocal) ? ConfigurationManager.AppSettings["ApiLocalUrl"].ToString() : ConfigurationManager.AppSettings["ApiLiveUrl"].ToString();
			HttpClient client = new HttpClient();
			try
			{
				client.BaseAddress = new Uri(apiUrl);
				if (HttpContext.Current.Session["Email"] != null)
			{
				var email = HttpContext.Current.Session["Email"].ToString();
				client.DefaultRequestHeaders.Add("Email",email);
				
			}
		}
			catch (Exception ex)
			{

				throw ex;
			}
       	client.DefaultRequestHeaders.Add("apiKey", "aYuihsf#%*HJl09");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpContent _Body = null;
            HttpMethod _Method = new HttpMethod(Method);
            HttpResponseMessage response;

            if (!string.IsNullOrEmpty(Body))
            {
                _Body = new StringContent(Body);
                _Body.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            switch (_Method.ToString().ToUpper())
            {
                case "GET":
                case "HEAD":
                    response = client.GetAsync(Url).Result;
                    break;
                case "POST":
                    {
                        response = client.PostAsync(Url, _Body).Result;
                    }
                    break;
                case "PUT":
                    {
                        response = client.PutAsync(Url, _Body).Result;
                    }
                    break;
                case "DELETE":
                    response = client.DeleteAsync(Url).Result;
                    break;
                default:
                    throw new NotImplementedException();
            }
            String content = response.Content.ReadAsStringAsync().Result;

			return JsonConvert.DeserializeObject<T>(content);
            
        }

        public static T InvokePageList<T>(string Url, string Body, string Method)
        {
            string apiUrl = "";
            if (HttpContext.Current.Request.IsLocal)
            {
                apiUrl = "http://localhost:54378/";
            }
            else
            {
                apiUrl = "http://postapi.technocodz.com/";
            }
            //string apiUrl = (HttpContext.Current.Request.IsLocal) ? ConfigurationManager.AppSettings["ApiLocalUrl"].ToString() : ConfigurationManager.AppSettings["ApiLiveUrl"].ToString();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Add("apiKey", "aYuihsf#%*HJl09");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpContent _Body = null;
            HttpMethod _Method = new HttpMethod(Method);
            HttpResponseMessage response;

            if (!string.IsNullOrEmpty(Body))
            {
                _Body = new StringContent(Body);
                _Body.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            switch (_Method.ToString().ToUpper())
            {
                case "GET":
                case "HEAD":
                    response = client.GetAsync(Url).Result;
                    break;
                case "POST":
                    {
                        response = client.PostAsync(Url, _Body).Result;
                    }
                    break;
                case "PUT":
                    {
                        response = client.PutAsync(Url, _Body).Result;
                    }
                    break;
                case "DELETE":
                    response = client.DeleteAsync(Url).Result;
                    break;
                default:
                    throw new NotImplementedException();
            }
            String content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(content);

        }
    }
}
