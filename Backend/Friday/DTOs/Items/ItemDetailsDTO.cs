using Friday.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.DTOs.Items
{
    /// <summary>
    /// DTO that contains data to make a new instance of ItemDetails
    /// </summary>
    public class ItemDetailsDTO
    {
        /// <summary>
        /// Serving size for this Item. Contains both the quantity and the unit. E.g. '100ml', '300g' or '1 full pizza'.
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// How many calories are in 1 serving. 
        /// </summary>
        public double Calories { get; set; }
        /// <summary>
        /// How many sugar is in 1 serving. Unit is arbitrary.
        /// </summary>
        public double SugarContent { get; set; }
        /// <summary>
        /// How many salt is in 1 serving. Unit is arbitrary.
        /// </summary>
        public double SaltContent { get; set; }
        /// <summary>
        /// What allergens can be found in this Item, stored in a single string.
        /// </summary>
        public string Allergens { get; set; }

        /// <summary>
        /// Constructs a new ItemDetails instance based on the data in this DTO.
        /// </summary>
        /// <returns></returns>
        public ItemDetails ToItemDetails()
        {
            return new ItemDetails { Size = Size, Calories = Calories, SugarContent = SugarContent, SaltContent = SaltContent, Allergens = Allergens };
        }
    }
}
