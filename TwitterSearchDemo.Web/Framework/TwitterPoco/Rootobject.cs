namespace TwitterSearchDemo.Framework.TwitterPoco
{
    public class Rootobject
    {
        public float completed_in { get; set; }
        public long max_id { get; set; }
        public string max_id_str { get; set; }
        public string next_page { get; set; }
        public int page { get; set; }
        public string query { get; set; }
        public string refresh_url { get; set; }
        public TwitterResult[] results { get; set; }
        public int results_per_page { get; set; }
        public int since_id { get; set; }
        public string since_id_str { get; set; }
    }
}