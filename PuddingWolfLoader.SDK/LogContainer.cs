using System;

namespace PuddingWolfLoader.SDK
{
    public class LogContainer
    {
        public delegate void LogAddedHandler(Type fromType,string text);

        public event LogAddedHandler OnLogAdded;

        public readonly Type FromType;

        public LogContainer(Type from){ 
            FromType = from;
        }

        public void Push(string text)
        {
            OnLogAdded.Invoke(FromType,text);
        }

        public static LogContainer CreateInstance<T>()
        {
            return new LogContainer(typeof(T));
        }
    }
}
