using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using Playground.ViewModels;
using Xamarin.Forms;

namespace Playground.Views {
    public class GridListView<T, TViewModel> : ListView where TViewModel : ViewModel<T, View> {
        public DataTemplate ColumnTemplate { get; set; }

        public static readonly BindableProperty ColumnSizeProperty =
            BindableProperty.Create(nameof(Items),
                                    typeof(int),
                                    typeof(GridListView<T, TViewModel>),
                                    3,
                                    propertyChanged: ItemsChanged);

        public int ColumnSize {
            get => (int)GetValue(ColumnSizeProperty);
            set => SetValue(ColumnSizeProperty, value);
        }

        public static readonly BindableProperty ItemsProperty =
            BindableProperty.Create(nameof(Items),
                                    typeof(IEnumerable),
                                    typeof(GridListView<T, TViewModel>),
                                    null,
                                    propertyChanged: ItemsChanged);


        public IEnumerable Items {
            get => (IEnumerable)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        public static readonly BindableProperty ColumnTappedProperty =
            BindableProperty.Create("ColumnTapped",
                                    typeof(Command<T>),
                                    typeof(GridListView<T, TViewModel>),
                                    null);


        public Command<T> ColumnTapped {
            get => (Command<T>)GetValue(ColumnTappedProperty);
            set => SetValue(ColumnTappedProperty, value);
        }

        public GridListView() {
            ItemTemplate = new DataTemplate(() => {
                return new MultiColumnRow(this);
            });
            ItemTapped += (sender, e) => {
                if (e.Item == null) return;
                SelectedItem = null;
            };
        }

        static void ItemsChanged(BindableObject bindable, object oldValue, object newValue) {
            var gridList = bindable as GridListView<T, TViewModel>;
            if (gridList.Items != null) {
                if (gridList.Items is INotifyCollectionChanged) {
                    ((INotifyCollectionChanged)gridList.Items).CollectionChanged += (sender, e) => {
                        gridList.ResetList();
                    };
                }
                gridList.ResetList();
            }
        }


        void ResetList() {
            var Rows = new List<object[]>();

            var ItemsE = Items.GetEnumerator();
            var col = ColumnSize;
            var currentRow = new object[col];
            var k = 0;
            while (ItemsE.MoveNext()) {
                if (k % col == 0 && k != 0) {
                    Rows.Add(currentRow);
                    currentRow = new object[col];
                }
                currentRow[k % col] = ItemsE.Current;
                k++;
            }
            if (currentRow[0] != null)
                Rows.Add(currentRow);

            ItemsSource = Rows;
        }


        sealed class MultiColumnRow : ViewCell {

            GridListView<T, TViewModel> ListView { get; }

            Grid Grid { get => View as Grid; }

            object[] Columns { get => BindingContext as object[]; }

            DataTemplate Template { get => ListView.ColumnTemplate; }

            public MultiColumnRow(GridListView<T, TViewModel> listview) {
                ListView = listview;
                var cd = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };
                Grid grid = new Grid { RowDefinitions = { new RowDefinition { Height = GridLength.Auto } }, };
                grid.VerticalOptions = LayoutOptions.Fill;
                for (var i = 0; i < ListView.ColumnSize; i++) {
                    grid.ColumnDefinitions.Add(cd);
                    grid.Children.Add(GenerateView(), i, 0);
                }

                View = grid;
            }

            View GenerateView() {
                var v = (View)Template.CreateContent();
                var tapped = new TapGestureRecognizer();
                tapped.Tapped += layoutTapped;
                v.GestureRecognizers.Add(tapped);
                return v;
            }

            void layoutTapped(object sender, EventArgs e) {
                var index = Grid.Children.IndexOf(sender as View);
                var columnClicked = Columns[index];
                var command = ListView.ColumnTapped;

                if (command != null) {
                    command.Execute(columnClicked);
                }
            }

            protected async override void OnBindingContextChanged() {
                base.OnBindingContextChanged();

                if (Columns != null && Grid.Children.Count() > 1) {
                    for (var i = 0; i < Columns.Length; i++) {
                        var col = Columns[i];
                        var view = Grid.Children.ToList()[i];
                        if (col != null) {
                            view.IsVisible = true;
                            var binding = Activator.CreateInstance(typeof(TViewModel), col, view);
                            Grid.Children.ToList()[i].BindingContext = binding;
                        } else {
                            view.IsVisible = false;
                        }
                    }
                }
            }
        }
    }
}
