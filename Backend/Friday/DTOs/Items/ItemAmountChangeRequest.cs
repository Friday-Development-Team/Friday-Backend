using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.DTOs.Items
{
    /// <summary>
    /// Request object for an item amount change.
    /// </summary>
    public class ItemAmountChangeRequest
    {
        /// <summary>
        /// ID of the item
        /// </summary>
        [Required]
        public int Id { get; set; }
        /// <summary>
        /// Amount to add
        /// </summary>
        [Required]
        public int Amount { get; set; }
    }
}
