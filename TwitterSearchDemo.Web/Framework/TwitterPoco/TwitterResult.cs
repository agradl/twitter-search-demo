using System;

namespace TwitterSearchDemo.Framework.TwitterPoco
{
    public class TwitterResult
    {
        public DateTime created_at { get; set; }
        public Entities entities { get; set; }
        public string from_user { get; set; }
        public int from_user_id { get; set; }
        public string from_user_id_str { get; set; }
        public string from_user_name { get; set; }
        public object geo { get; set; }
        public long id { get; set; }
        public string id_str { get; set; }
        public string iso_language_code { get; set; }
        public Metadata metadata { get; set; }
        public string profile_image_url { get; set; }
        public string profile_image_url_https { get; set; }
        public string source { get; set; }
        public string text { get; set; }
        public string to_user { get; set; }
        public int to_user_id { get; set; }
        public string to_user_id_str { get; set; }
        public string to_user_name { get; set; }
        public long in_reply_to_status_id { get; set; }
        public string in_reply_to_status_id_str { get; set; }
    }
}