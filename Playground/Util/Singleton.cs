using System;
namespace Playground.Util {
    public class Singleton {
        static Singleton _Instance;

        public static Singleton Instance {
            get {
                if (_Instance == null) {
                    _Instance = new Singleton();
                }
                return _Instance;
            }
        }
    }
}
