using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TwitterSearchDemo.Framework.Web;

namespace TwitterSearchDemo.Framework
{
    public class TwitterSearchQuery
    {
        [Required]
        public string Query { get; set; }

        [EnumRadio(typeof (ResultType))]
        public ResultType ResultType { get; set; }

        [EnumRadio(typeof (SearchType))]
        public SearchType SearchType { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Page { get; set; }

        protected bool Equals(TwitterSearchQuery other)
        {
            return string.Equals(Query, other.Query) && ResultType == other.ResultType && SearchType == other.SearchType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TwitterSearchQuery) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Query != null ? Query.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (int) ResultType;
                hashCode = (hashCode*397) ^ (int) SearchType;
                return hashCode;
            }
        }

        public TwitterSearchQuery Copy(int pageDiff)
        {
            return new TwitterSearchQuery
                       {
                           Query = Query,
                           ResultType = ResultType,
                           SearchType = SearchType,
                           Page = Page + pageDiff
                       };
        }
    }

    public enum SearchType
    {
        [Description("#")] Hashtag,
        [Description("@")] User,
        [Description("text")] Text
    }

    public enum ResultType
    {
        [Description("recent")] Recent,
        [Description("popular")] Popular,
        [Description("both")] Mixed
    }
}