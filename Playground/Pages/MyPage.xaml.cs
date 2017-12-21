using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Playground.Models;

namespace Playground.Pages {
    public partial class MyPage : ContentPage {

        public MyPage() {


            InitializeComponent();
          

            var list = new List<ShitModel>();

            var person = new ShitModel();
            person.Name = "Harry";


            list.Add(person);
            CarouselView.ItemsSource = list;
        }



    }
}