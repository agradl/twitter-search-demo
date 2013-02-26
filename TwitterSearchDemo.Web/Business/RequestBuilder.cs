using System.Globalization;
using System.Text;
using System.Web;
using TwitterSearchDemo.Framework;

namespace TwitterSearchDemo.Business
{
    public class RequestBuilder : IBuildRequestUrl
    {
        public const string BaseUrl = "http://search.twitter.com/search.json";

        public string GetUrlFor(TwitterSearchQuery criteria, int page, int pageSize)
        {
            var builder = new StringBuilder();
            builder.Append(BaseUrl);
            builder.Append("?q=");
            var queryText = "";
            if (criteria.SearchType == SearchType.Text)
            {
               queryText += string.Format(@"""{0}""", criteria.Query);
            }
            else
            {
                queryText += (criteria.SearchType == SearchType.Hashtag ? "#" : "@") + criteria.Query;
            }

            builder.Append(HttpUtility.UrlEncode(queryText));

            builder.Append("&rpp=" + pageSize.ToString(CultureInfo.InvariantCulture));
            builder.Append("&result_type=" + criteria.ResultType);
            builder.Append("&page=" + page.ToString(CultureInfo.InvariantCulture));

            return builder.ToString();
        }
    }
}