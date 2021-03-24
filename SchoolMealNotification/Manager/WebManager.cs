using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SchoolMealNotification.Model;
using SchoolMealNotification.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SchoolMealNotification.Manager
{
    class WebManager
    {
        public T webRequest<T>(QParamModel[] query, string resource)
        {
            WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;

            string uri = Settings.Default.apiUri + resource;
            if (query != null)
            {
                uri += "?";
                foreach (QParamModel qParam in query)
                {
                    uri += qParam.key + "=" + qParam.value;
                    // 마지막 쿼리가 아니라면 구분자 추가
                    if (query.GetValue(query.Length - 1) != qParam)
                    {
                        uri += "&";
                    }
                }
            }

            Stream data = webClient.OpenRead(uri);
            StreamReader reader = new StreamReader(data);
            JObject jObject = JObject.Parse(reader.ReadToEnd());
            var result = JsonConvert.DeserializeObject<T>(jObject.ToString());

            return result;
        }
    }
}
