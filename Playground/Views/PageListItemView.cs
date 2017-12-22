﻿using System;
using Xamarin.Forms;
using FFImageLoading.Work;
namespace Playground.Views {
    public class PageListItemView : ViewCell{

        public PageListItemView(){
            
        }
        protected override void OnBindingContextChanged() {
            base.OnBindingContextChanged();
            if (BindingContext!=null){
                var page = BindingContext as Type;
                View = new Label {
                    Text = page.Name
                };
            }
        }
    }
}
