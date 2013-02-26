using System.Net;
using Newtonsoft.Json;
using TwitterSearchDemo.Framework;
using TwitterSearchDemo.Framework.TwitterPoco;

namespace TwitterSearchDemo.Business
{
    public class TwitterApi : ICallTwitterApi
    {
        public Rootobject GetDataFor(string url)
        {
            return GetResultsFor(new WebClient().DownloadString(url));
        }

        private Rootobject GetResultsFor(string rawResult)
        {
            return JsonConvert.DeserializeObject<Rootobject>(rawResult);
        }
    }
}