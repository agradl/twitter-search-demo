using TwitterSearchDemo.Business;
using TwitterSearchDemo.Framework.TwitterPoco;

namespace TwitterSearchDemo.Framework
{
    public interface ICallTwitterApi
    {
        Rootobject GetDataFor(string url);
    }
}