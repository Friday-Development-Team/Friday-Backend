using Friday.Models;
using Friday.Models.Logs;
using System.Collections.Generic;

namespace Friday.DTOs.Items
{
    /// <summary>
    /// DTO that contains data to make a new Item instance.
    /// </summary>
    public class ItemDTO
    {
        /// <summary>
        /// Name of the Item
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Price of the Item. Currency is arbitrary
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// Type of this Item. Will get converted to an ItemType enum value
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// How many of this Item are initially in stock
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// ItemDetails data for this Item
        /// </summary>
        public ItemDetailsDTO Details { get; set; }
        /// <summary>
        /// Path to the image connected to this Item. Dependant on frontend
        /// </summary>
        public string ImageName { get; set; }

        /// <summary>
        /// Constructs a new Item instance based on the data of this object.
        /// </summary>
        /// <returns></returns>
        public Item ToItem()
        {
            return new Item { Name = Name, Price = Price, Type = ItemTools.FromString(Type), NormalizedImageName = ImageName, Count = Count, Logs = new List<ItemLog>() };
        }
    }
}
