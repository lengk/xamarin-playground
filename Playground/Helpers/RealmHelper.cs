using System;
using System.Diagnostics;
using Realms;

namespace Playground.Helpers {
    public static class RealmHelper {
        public static Realm Instance {
            get {
                var config = new RealmConfiguration(Constants.REALM_FILE);
                config.ShouldDeleteIfMigrationNeeded = true;
                Realm instance;
                try {
                    instance = Realm.GetInstance(config);
                    return instance;
                } catch (System.Reflection.ReflectionTypeLoadException err) {
                    Debug.WriteLine(err);
                } catch (Exception e) {
                    Debug.WriteLine(e.Message);
                    Realm.DeleteRealm(config);
                    return Realm.GetInstance(config);
                }
                return null;
            }
        }
    }
}
