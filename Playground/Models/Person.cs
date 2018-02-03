using System;
using Realms;
namespace Playground.Models {
    public class Person : RealmObject {
        [PrimaryKey]
        public string ID { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string ImageUrl { get; set; }

        public override string ToString() {
            return string.Format("[Person: ID={0}, Name={1}, Age={2}, ImageUrl={3}]", ID, Name, Age, ImageUrl);
        }
    }
}
