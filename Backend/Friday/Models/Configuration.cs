using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.Models {
    public class Configuration {
        public int Id { get; set; }
        public bool CombinedCateringKitchen { get; set; }
        public bool UsersSetSpot { get; set; }
        public bool CancelOnAccepted { get; set; }

        public void Copy(Configuration con) {
            CancelOnAccepted = con.CancelOnAccepted;
            CombinedCateringKitchen = con.CombinedCateringKitchen;
            UsersSetSpot = con.UsersSetSpot;
        }

    }
}
