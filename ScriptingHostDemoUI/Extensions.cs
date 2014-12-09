using System;
using System.Collections.Generic;
using System.IO.Packaging;

namespace App
{
    public static  class Extensions
    {
        public static IEnumerable<T> WithEach<T>(this IEnumerable<T> ts, Action<T> action, Func<T, bool> condition = null)
        {
            condition = condition ?? (x => true); //?? (x => !Equals(default(T), x)); 

            foreach (var t in ts)
            {
               if(condition(t))
                    action(t);
                yield return t;
            }
        }

        public static bool HasValue<T>(this T t) 
        {
            return !Equals(t, default(T));
        }
    }
}
