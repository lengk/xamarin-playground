using System;
namespace Playground.Util {
    public abstract class Singleton<T> where T : Singleton<T>, new() {
        static T _instance = new T();
        public static T Instance {
            get {
                return _instance;
            }
        }
    }
}
