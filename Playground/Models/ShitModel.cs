using System;
using Realms;

namespace Playground.Models {
    public class ShitModel : RealmObject {
        
        public string Name { get; set; }

        public override string ToString() {
            return string.Format("[ShitModel: Name={0}]", Name);
        }
    }
}
