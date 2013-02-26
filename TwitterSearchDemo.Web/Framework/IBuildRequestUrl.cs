namespace TwitterSearchDemo.Framework
{
    public interface IBuildRequestUrl
    {
        string GetUrlFor(TwitterSearchQuery query, int page, int pageSize);
    }
}