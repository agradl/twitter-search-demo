namespace TwitterSearchDemo.Framework.TwitterPoco
{
    public class User_Mentions
    {
        public string screen_name { get; set; }
        public string name { get; set; }
        public int id { get; set; }
        public string id_str { get; set; }
        public int[] indices { get; set; }
    }
}