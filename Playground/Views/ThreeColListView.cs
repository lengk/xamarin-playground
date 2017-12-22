using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;
using Xamarin.Forms.Xaml;
using System.Windows.Input;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Realms;
using Playground.Models;

namespace Playground.Views {
    public class ThreeColListView : ListView {
        public DataTemplate ColumnTemplate { get; set; }

        public static readonly BindableProperty ItemsProperty =
            BindableProperty.Create(nameof(Items),
                                    typeof(IEnumerable),
                                    typeof(ThreeColListView),
                                    null,
                                    propertyChanged: ItemsChanged);


        public IEnumerable Items {
            get => (IEnumerable)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        public static readonly BindableProperty ColumnTappedProperty =
            BindableProperty.Create("ColumnTapped",
                                    typeof(ICommand),
                                    typeof(ThreeColListView),
                                    null);

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public ICommand ColumnTapped {
            get => (ICommand)GetValue(ColumnTappedProperty);
            set => SetValue(ColumnTappedProperty, value);
        }
        
        public ThreeColListView() {
            CollectionChanged += (sender, e) => {
                Debug.WriteLine("Fucked");
            };
            ItemTemplate = new DataTemplate(() => {
                return new MultiColumnRow(this);
            });
        }

     

        static void ItemsChanged(BindableObject bindable, object oldValue, object newValue) {
            var x = bindable as ThreeColListView;

            if (x.Items is INotifyCollectionChanged){
                ((INotifyCollectionChanged)x.Items).CollectionChanged += (sender, e) => {
                    x.ResetList();
                };
            }
            x.ResetList();
        }


        void ResetList(){
            var Rows = new List<object[]>();

            var ItemsE = Items.GetEnumerator();

            var currentRow = new object[3];
            var k = 0;
            while (ItemsE.MoveNext()) {
                if (k % 3 == 0 && k != 0) {
                    Rows.Add(currentRow);
                    currentRow = new object[3];
                }
                currentRow[k % 3] = ItemsE.Current;
                k++;
            }
            if (currentRow[0] != null)
                Rows.Add(currentRow);

            ItemsSource = Rows;
        }


        private sealed class MultiColumnRow : ViewCell {

            ThreeColListView ListView { get; }

            Grid Grid { get => View as Grid; }

            object[] Columns { get => BindingContext as object[]; }

            DataTemplate Template { get => ListView.ColumnTemplate; }

            public MultiColumnRow(ThreeColListView listview) {
                ListView = listview;
                var cd = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };
                Grid grid = new Grid { RowDefinitions = { new RowDefinition { Height = GridLength.Auto } }, };

                for (var i = 0; i < 3; i++) {
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

                if (command != null){
                    command.Execute(columnClicked);
                }
            }

            protected override void OnBindingContextChanged() {
                base.OnBindingContextChanged();

                if (Columns != null && Grid.Children.Count() > 1) {
                    for (var i = 0; i < Columns.Length; i++) {
                        Grid.Children.ToList()[i].BindingContext = Columns[i];
                    }
                }
            }
        }
    }
}
