using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SportsStore.Models
{
    public static class Extensions
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list) 
        {
            IList<T> result = list.ToList();
            int length = result.Count(); 
            Random r = new Random();
            for (int i = 0; i < length; i++)
            {
                int j = r.Next(length);
                T t = result[j];
                result.RemoveAt(j);
                result.Add(t);
            }
            return result;
        }
    }
}
