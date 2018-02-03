using System;
using System.ComponentModel;
using Playground.Helpers;
using Realms;
using Xamarin.Forms;

namespace Playground.ViewModels {
    public class ViewModel<TView> : ViewModel<object, TView> {
        public ViewModel(TView v, object model = null) : base(model, v) { }
    }

    public class ViewModel<T, TView> : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected Realm Realm = RealmHelper.Instance;
        public T Model { get; set; }
        protected TView View { get; set; }

        public ViewModel(T model, TView v) {
            Model = model;
            View = v;
        }

        public void NotifyChanged(string propertyname) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }

}
