using System;
using static PuddingWolfLoader.SDK.Model.LogModel;

namespace PuddingWolfLoader.SDK
{
    public class LogContainer
    {
        public delegate void LogAddedHandler(PudLog log);

        public event LogAddedHandler OnLogAdded;

        public readonly Type FromType;

        public LogContainer(Type from)
        {
            FromType = from;
        }

        public void Push(PudLog log)
        {
            OnLogAdded?.Invoke(log);
        }

        /// <summary>
        /// 一条Info日志
        /// </summary>
        /// <param name="text"></param>
        public void Push(string text)
        {
            Info(text);
        }
        public void Info(string text)
        {
            Push(new PudLog(PudLogType.Info, FromType.Name, DateTime.Now, text));
        }

        public void Debug(string text)
        {
            Push(new PudLog(PudLogType.Debug, FromType.Name, DateTime.Now, text));

        }

        public void Error(string text)
        {
            Push(new PudLog(PudLogType.Error, FromType.Name, DateTime.Now, text));
        }

        public void Warning(string text)
        {
            Push(new PudLog(PudLogType.Warning, FromType.Name, DateTime.Now, text));
        }

        public void Fatal(string text)
        {
            Push(new PudLog(PudLogType.Fatal, FromType.Name, DateTime.Now, text));
        }


        public static LogContainer CreateInstance<T>()
        {
            return new LogContainer(typeof(T));
        }
    }
}
