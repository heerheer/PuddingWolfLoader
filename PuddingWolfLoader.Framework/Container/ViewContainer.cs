using System;
using System.Collections.Generic;
using System.Linq;

namespace PuddingWolfLoader.Framework.Container
{
    class ViewContainer
    {
        private static Dictionary<Type, object> _views = new Dictionary<Type, object>();

        public static object GetViewInstance<T>()
        {
            var type = typeof(T);
            return GetViewInstance(type);
        }

        private static object GetViewInstance(Type type)
        {
            if (_views.ContainsKey(type))
            {
                if (_views[type] is null)
                {
                    RegisterViewInstance(type);
                }
                return _views[type];
            }
            else
            {
                RegisterViewInstance(type);
            }
            return _views[type];
        }

        public static void RegisterViewInstance(Type type)
        {
            _views.Add(type, Activator.CreateInstance(type) ?? new object());
        }

        public static object GetViewInstance(int id)
        {
            Type type;
            if (id >= _views.Count)
            {
                //数组越界
                type = _views.ToList().First().Key;
            }
            else
            {
                type = _views.ToList()[id].Key;
            }
            return GetViewInstance(type);
        }
    }
}
