using System.Reflection;
using System.Web.Routing;

namespace TwitterSearchDemo.Framework.Web
{
    public static class Extensions
    {
        public static RouteValueDictionary ToRouteValues(this object obj)
        {
            var result = new RouteValueDictionary();
            foreach (PropertyInfo property in obj.GetType().GetProperties())
            {
                object val = property.GetValue(obj);
                string name = property.Name;
                if (val != null)
                {
                    result.Add(name, val);
                }
            }
            return result;
        }
    }
}