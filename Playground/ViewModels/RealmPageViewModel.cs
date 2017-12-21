using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Playground.Helpers;
using Playground.Models;
using Realms;

namespace Playground.ViewModels {
    public class RealmPageViewModel : INotifyPropertyChanged {
        readonly Realm realm;
        RealmPage Page;

        public RealmPageViewModel(RealmPage p) {
            Page = p;
            realm = Realm.GetInstance("playground.realm");
            using (var trans = realm.BeginWrite()) {
                for (var i = 0; i < 20; i++)
                    realm.Add(new ShitModel {
                        Name = "" + i
                    });
                trans.Commit();
            }
            SomeShit = realm.All<ShitModel>();

       
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name) {
            PropertyChanged?.Invoke(Page, new PropertyChangedEventArgs(name));
        }

        public IEnumerable<ShitModel> SomeShit { get; set; }

        public void Add() {
            using (var trans = realm.BeginWrite()) {
                Realm.GetInstance("playground.realm").Add(new ShitModel {
                    Name = "" + SomeShit.Count()
                });
                trans.Commit();
            }
        }

        public void Clear() {
            using (var trans = realm.BeginWrite()) {
                Realm.GetInstance("playground.realm").RemoveAll();
                trans.Commit();
            }
        }

    }
}
