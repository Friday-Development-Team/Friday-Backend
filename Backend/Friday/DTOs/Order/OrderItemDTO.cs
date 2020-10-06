using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.DTOs {
    /// <summary>
    /// Contains the data for a single item in an order and how much was ordered.
    /// </summary>
    public class OrderItemDTO {
        /// <summary>
        /// ID of the Item
        /// </summary>
        [Required]
        public int Id { get; set; }
        /// <summary>
        /// How much was ordered
        /// </summary>
        [Required]
        public int Amount { get; set; }

    }
}
