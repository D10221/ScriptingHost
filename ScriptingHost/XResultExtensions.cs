using System;

namespace ScriptingHost
{
    public static class XResultExtensions
    {
        public static Tuple<T, Exception> OnError<T>(this Tuple<T, Exception> result, Action<Exception> onError)
        {
            if (result.Item2 != null)
                onError(result.Item2);
            return result;
        }

        public static Tuple<T, Exception> OnSuccess<T>(this Tuple<T, Exception> result, Action<T> onSuccess)
        {
            if (result.Item2 == null)
            {
                onSuccess(result.Item1);
            }
            return result;
        }

        public static Tuple<R,Exception> OnSuccess<T,R>(this Tuple<T,Exception> result, Func<T,R> onSuccess)
        {
            R x = default(R);
            
            Exception ex = null;

            if (result.Item2 == null)
            {

                try
                {
                    x = onSuccess(result.Item1);
                }
                catch (Exception e)
                {
                    ex = new Exception("?", e);
                }
            }

            return new Tuple<R, Exception>(x,ex ?? result.Item2 );
        }
    }
}