using System;
using Realms;
namespace Playground.Models {
    public class Person : RealmObject{
        public string Name { get; set; }
        public string Age { get; set; }
        public string ImageUrl { get; set; }
    }
}
