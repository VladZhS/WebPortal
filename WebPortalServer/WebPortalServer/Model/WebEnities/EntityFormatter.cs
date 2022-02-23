    using System;
using System.Collections.Generic;

namespace WebPortalServer.Model.WebEnities
{
    public static class EntityFormatter
    {
        //Converts IEnumerable<T> to ICollection<K> (T MUST be a parameter of K's constructor)
        public static ICollection<K> ConvertModel<T, K>(this IEnumerable<T> collection) where K : BaseModel<T>
        {
            ICollection<K> result = new List<K>();
            try
            {
                foreach (T item in collection)
                {
                    result.Add((K)Activator.CreateInstance(typeof(K), item));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("T must be a parameter of K's constructor", ex);
            }
            return result;
        }
    }
}
