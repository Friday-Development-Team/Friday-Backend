using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.Models {
    public class ShopUser {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }

        public bool UpdateBalance(double amount) {
            if (amount < 0 && Math.Abs(amount) > Balance)//If negative and would reduce balance below zero. 
                return false;
            var original = Balance;
            Balance += amount;
            return Balance != original;
        }
    }
}
