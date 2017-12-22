using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Playground.ViewModels;
using Playground.Helpers;
using Realms;
using Playground.Models;
using System.Linq;
using System.Diagnostics;

namespace Playground.Pages {
    public partial class MultiColPage : ContentPage {
        Realm realm { get => RealmHelper.Instance; }
        public MultiColPage() {
            BindingContext = new MultiColPageViewModel(this);
            InitializeComponent();
            List.Items = realm.All<Person>();
        }

        void AddClicked(object sender, System.EventArgs e) {
            realm.Write(() => {
                var person = new Person() {
                    Name = "asda"
                };
                realm.Add(person, false);
            });
            
            Debug.WriteLine(realm.All<Person>().ToList().Count());
        }

        void RemoveClicked(object sender, EventArgs args) {
            Random r = new Random();
            realm.Write(() => {
                var people = realm.All<Person>().ToList();
                var i = r.Next(people.Count());
                realm.Remove(people[i]);
            });
            Debug.WriteLine(realm.All<Person>().ToList().Count());
        }
    }
}
