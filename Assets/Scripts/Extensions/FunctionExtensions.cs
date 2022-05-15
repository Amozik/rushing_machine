using System;

namespace RushingMachine.Extensions
{
    public static class FunctionExtensions
    {
        public static Func<TResult> Bind<T, TResult>(Func<T, TResult> func, T arg)
        {
            return () => func(arg);
        }
        
        public static Action Bind<T>(Action<T> action, T arg) 
        {
            return () => action(arg);
        }
        
        public static Action<T2> Bind<T1, T2>(Action<T1, T2> action, T1 arg)
        {
            return t2 => action(arg, t2);
        }
    }
}