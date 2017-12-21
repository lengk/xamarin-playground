using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;
using Xamarin.Forms.Xaml;
using System.Windows.Input;

namespace Playground.Views {
    public class ThreeColListView<T> : ListView {

        public DataTemplate ColumnTemplate { get; set; }

        public static readonly BindableProperty ItemsProperty =
            BindableProperty.Create(nameof(Items),
                                    typeof(IEnumerable<T>),
                                    typeof(ThreeColListView<T>),
                                    null,
                                    propertyChanged: ItemsChanged);


        public IEnumerable<T> Items {
            get => (IEnumerable<T>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        public static readonly BindableProperty ColumnTappedProperty =
            BindableProperty.Create("ColumnTapped",
                                    typeof(Command<T>),
                                    typeof(ThreeColListView<T>),
                                    null);

        public ICommand ColumnTapped {
            get => (ICommand)GetValue(ColumnTappedProperty);
            set => SetValue(ColumnTappedProperty, value);
        }
        
        public ThreeColListView() {
            ItemTemplate = new DataTemplate(() => {
                return new MultiColumnRow(this);
            });
        }

        static void ItemsChanged(BindableObject bindable, object oldValue, object newValue) {
            var x = bindable as ThreeColListView<T>;
            var Rows = new List<T[]>();
            var Items = new List<T>(x.Items);

            var currentRow = new T[3];

            for (var k = 0; k < Items.Count(); k++) {
                if (k % 3 == 0 && k != 0) {
                    Rows.Add(currentRow);
                    currentRow = new T[3];
                }
                currentRow[k % 3] = Items[k];
            }

            x.ItemsSource = Rows;
        }


        private sealed class MultiColumnRow : ViewCell {

            ThreeColListView<T> ListView { get; }

            Grid Grid { get => View as Grid; }

            T[] Columns { get => BindingContext as T[]; }

            DataTemplate Template { get => ListView.ColumnTemplate; }

            public MultiColumnRow(ThreeColListView<T> listview) {
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
                T columnClicked = Columns[index];
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
