using System;
using Playground.Models;
using Xamarin.Forms;

namespace Playground.ViewModels.ViewCells {
    public class GridListCellVM : ViewModel<Person, View> {
        public GridListCellVM(Person model, View v) : base(model, v) {
        }
    }
}
