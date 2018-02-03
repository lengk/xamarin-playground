using System;

using Xamarin.Forms;
using Playground.Helpers;
using System.Threading.Tasks;
using Playground.Models;
using System.Diagnostics;
using Realms;
using FFImageLoading.Work;

namespace Playground.Pages {
    public partial class RealmMultiThreadAccess : ContentPage {
        int count = 0;
        public Realm realm = RealmHelper.Instance;

        public RealmMultiThreadAccess() {
            Content = new Button() {
                Text = "Show Person"
            };
            realm.Write(realm.RemoveAll<Person>);
            Person person = realm.Find<Person>("1");
            if (person == null) {
                person = new Person();
                person.ID = "1";
                person.Name = "Harry" + count.ToString();
                realm.Write(() => {
                    realm.Add(person);
                });
            }
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            while (count < 10) {
                count++;
                Task.Run(() => HandleAction(count));
            }
        }


        void HandleAction(int count) {
            Person person = realm.Find<Person>("1");
            person.Name = "Harry" + count.ToString();
            RealmHelper.Instance.Write(() => {
                try {
                    realm.Add(person);
                } catch (Exception e) {
                    Debug.WriteLine(e);
                }
            });
            
        }

        public void ButtonClicked(object sender, EventArgs e) {
            var p = realm.Find<Person>("1");
            Debug.WriteLine(p);
        }
    }
}

