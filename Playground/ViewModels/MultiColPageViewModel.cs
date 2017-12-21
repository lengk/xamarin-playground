using System.Collections.Generic;
using Playground.Models;
using Xamarin.Forms;
using System.ComponentModel;
using System.Linq;
using Playground.Helpers;
using System.Windows.Input;
using System.Diagnostics;

namespace Playground.ViewModels {
    public class MultiColPageViewModel : INotifyPropertyChanged {
        public IEnumerable<Person> ListSource { get; set; }
        public ICommand ColumnTappedCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public MultiColPageViewModel(ContentPage page) {
            ColumnTappedCommand = new Command<Person>(ColumnClicked);
            var list = new List<Person>();
            for (var i = 0; i < 10; i++) {
                list.Add(new Person { Name = "Harry" + i });
            }
            ListSource = list;
        }

        void ColumnClicked(object obj) {
            Debug.WriteLine(((Person)obj).Name);
        }
    }
}

