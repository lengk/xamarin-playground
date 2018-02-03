using System.Collections.Generic;
using Playground.Models;
using Xamarin.Forms;
using System.ComponentModel;
using System.Linq;
using Playground.Helpers;
using System.Windows.Input;
using System.Diagnostics;
using Realms;

namespace Playground.ViewModels {
    public class MultiColPageViewModel: INotifyPropertyChanged {
        public IEnumerable<Person> ListSource { get; set; }
        public Command<Person> ColumnTappedCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MultiColPageViewModel(ContentPage page) {
            ColumnTappedCommand = new Command<Person>(ColumnClicked);
            ListSource = RealmHelper.Instance.All<Person>();
        }

        void ColumnClicked(Person obj) => Debug.WriteLine(obj.Name);
    }
}

