using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;
using static Top.Api.Response.TbkDgMaterialOptionalResponse;

namespace MaltDemo.Controllers
{
    public class Help
    {

        public long _appKey;
        public string _appSecret;

        public Help(long appKey, string appSecret)
        {
            _appKey = appKey;
            _appSecret = appSecret;
        }

        public TbkTpwdCreate GetNumids(string url)
        {
            string shophtml = HttpPostNew(url);

            Regex reg1 = new Regex(@"[a-zA-z]+://[^\s]*");
            Match match1 = reg1.Match(shophtml);

            // string shophtml1 = HttpPostNew(match1.Value);

            Regex reg = new Regex(@"https://a.m.taobao.com/i[0-9]*\?*");
            Match match = reg.Match(match1.Value);

            if (match.Value.Split('i').Length < 1)
            {
                return null;
            }

            string num_iids = match.Value.Split('i')[1];

            string title = GetInfo(num_iids);

            if (string.IsNullOrWhiteSpace(title))
            {
                return null;
            }

            var optional = GetOptional(title, num_iids);

            if (optional != null)
            {
                string taokoul = "https:" + optional.Url;

                var a = new TbkTpwdCreate();
                a.tbkTpwdCreateResponse = GetCreate(taokoul);
                a.mapDataDomain = optional;
                return a;
            }
            else
            {
                return null;
            }

        }

        public string GetInfo(string numIids)
        {
            ITopClient client = new DefaultTopClient("http://gw.api.taobao.com/router/rest", _appKey.ToString(), _appSecret);
            TbkItemInfoGetRequest req = new TbkItemInfoGetRequest();
            req.NumIids = numIids;
            TbkItemInfoGetResponse response = client.Execute(req);
            if (response.Results.Count == 0)
            {
                return null;
            }
            return response.Results[0].Title;
        }

        public MapDataDomain GetOptional(string title, string num_iids)
        {
            int pageindex = 1;
            long count = 0;
            string page_result_key = null;

            do
            {
                ITopClient client = new DefaultTopClient("http://gw.api.taobao.com/router/rest", _appKey.ToString(), _appSecret);
                TbkDgMaterialOptionalRequest req = new TbkDgMaterialOptionalRequest();
                req.Q = title;
                req.AdzoneId = 111426350068L;
                req.PageSize = 100;
                req.PageNo = pageindex;
                req.PageResultKey = page_result_key;
                TbkDgMaterialOptionalResponse response = client.Execute(req);
                count = response.TotalResults;
                page_result_key = response.PageResultKey;
                foreach (var item in response.ResultList)
                {
                    if (item.ItemId.ToString() == num_iids)
                    {
                        return item;
                    }
                }
            } while (count > (pageindex++ * 100));


            return null;
        }

        public TbkTpwdCreateResponse GetCreate(string taokoul)
        {
            ITopClient client = new DefaultTopClient("http://gw.api.taobao.com/router/rest", _appKey.ToString(), _appSecret);
            TbkTpwdCreateRequest req = new TbkTpwdCreateRequest();
            req.Url = taokoul;
            return client.Execute(req);
        }

        private string HttpPostNew(string Url)
        {

            HttpWebRequest request = WebRequest.Create(Url) as HttpWebRequest;//(HttpWebRequest)WebRequest.Create(Url);
            request.Method = "GET";

            request.ContentType = "application/json";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }


    }

    public class TbkTpwdCreate
    {
        public MapDataDomain mapDataDomain { get; set; }

        public TbkTpwdCreateResponse tbkTpwdCreateResponse { get; set; }

    }
}
