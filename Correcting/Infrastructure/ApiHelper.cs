using Correcting.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Correcting.Infrastructure
{
    public static class ApiHelper
    {
        public static readonly string ApiUrl = ConfigurationManager.AppSettings["ApiUrl"];
        public static dynamic GetLocations()
        {
            var header = new RequestHeaderHelper().GetHeader();
            var json = new
            {
                requests = new[] { new { url = "/data/rest/areas" } }
            };
            var jsonstring = JsonConvert.SerializeObject(json);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("signature", header.signature);
            client.DefaultRequestHeaders.Add("requestTime", header.requestTime);
            client.DefaultRequestHeaders.Add("random", header.random);

            var content = new StringContent(jsonstring);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = client.PostAsync(ApiUrl, content).Result;
            var resultString = response.Content.ReadAsStringAsync().Result;
            var model = JsonConvert.DeserializeObject<dynamic>(resultString);
            return model.result[0].result.areaList;
        }        
        public static dynamic GetLocation(string code)
        {
            var header = new RequestHeaderHelper().GetHeader();
            var json = new
            {
                requests = new[] {
                    new {
                        url = "/data/rest/areas/view",
                       @params= new
                        {
                           id = code
                        }
                    }
                }
            };
            var jsonstring = JsonConvert.SerializeObject(json);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("signature", header.signature);
            client.DefaultRequestHeaders.Add("requestTime", header.requestTime);
            client.DefaultRequestHeaders.Add("random", header.random);

            var content = new StringContent(jsonstring);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = client.PostAsync(ApiUrl, content).Result;
            var resultString = response.Content.ReadAsStringAsync().Result;
            var model = JsonConvert.DeserializeObject<dynamic>(resultString);
            return model.result[0].result.area;
        }
        public static IEnumerable<LocationModel> GetLocationSubs()
        {
            var model = GetLocations();
            List<LocationJsonModel> data = JsonConvert.DeserializeObject<List<LocationJsonModel>>(model.ToString());
            var all = data.Where(n => n.Id != "100000").ToList();
            var root = all.Where(n => n.Parent == null).ToList();
            var subs = all.Where(n => n.Parent != null).ToList();
            var result = new List<LocationModel>();
            Recursion(root, subs, result);
            return result;

        }
        /// <summary>
        /// 递归
        /// </summary>
        /// <param name="roots"></param>
        /// <param name="all"></param>
        /// <param name="models">结果</param>
        public static void Recursion(IEnumerable<LocationJsonModel> roots, List<LocationJsonModel> all, ICollection<LocationModel> models, LocationModel currentModel = null)
        {
            foreach (var root in roots)
            {
                var subModel = new LocationModel { Id = root.Id, Name = root.Name, FullName = root.FullName, ParentId = root.Parent == null ? null : root.Parent.Id };
                if (currentModel != null && currentModel.Id == root.Parent.Id)
                {
                    if (currentModel.SubModels == null)
                    {
                        currentModel.SubModels = new List<LocationModel>();
                    }
                    currentModel.SubModels.Add(subModel);

                }
                else
                {
                    models.Add(subModel);
                }
                var subs = all.Where(n => n.Parent.Id == root.Id).ToArray();
                if (subs.Any())
                {
                    Recursion(subs, all, models,subModel);
                }
            }
        }
    }
}