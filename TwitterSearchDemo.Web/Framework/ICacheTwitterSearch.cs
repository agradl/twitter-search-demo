using System.Collections.Generic;
using TwitterSearchDemo.Framework.TwitterPoco;

namespace TwitterSearchDemo.Framework
{
    public interface IManageTwitterSearch
    {
        List<TwitterResult> GetResultsFor(TwitterSearchQuery query);
    }
}