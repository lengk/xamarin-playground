using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Playground.Helpers;
using Playground.Models;
using Realms;

namespace Playground.ViewModels {
    public class RealmBindingPageViewModel : INotifyPropertyChanged {

        Realm realm { get => RealmHelper.Instance; }
        RealmBindingPage Page;

        public RealmBindingPageViewModel(RealmBindingPage p) {
            Page = p;
            realm.Write(() => {
                for (var i = 0; i < 20; i++)
                    realm.Add(new ShitModel {
                        Name = "" + i
                    });
            });
            SomeShit = realm.All<ShitModel>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name) {
            PropertyChanged?.Invoke(Page, new PropertyChangedEventArgs(name));
        }

        public IEnumerable<ShitModel> SomeShit { get; set; }

        public void Add() {
            using (var trans = realm.BeginWrite()) {
                realm.Add(new ShitModel {
                    Name = "" + SomeShit.Count()
                });
                trans.Commit();
            }
        }

        public void Clear() {
            using (var trans = realm.BeginWrite()) {
                realm.RemoveAll();
                trans.Commit();
            }
        }

    }
}
