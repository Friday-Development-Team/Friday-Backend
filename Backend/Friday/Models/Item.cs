using Friday.Models.Logs;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Friday.Models
{
    /// <summary>
    /// Class containing information about Items used in the shop. Contains only basic information, such as name, price, type (food or drink) and the amount left.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// ID of this object
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of this Item
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// How much this Item costs to order
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// What type of Item this is (food, beverage)
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ItemType Type { get; set; }
        /// <summary>
        /// How many instances of this Item are still in stock
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// Object containing all the details for this Item
        /// </summary>
        public ItemDetails ItemDetails { get; set; }
        /// <summary>
        /// Collection of all logs concerning this Item. Only used for EF, should always be made null.
        /// </summary>
        public ICollection<ItemLog> Logs { get; set; }
        /// <summary>
        /// The image name associated with this object. In the frontend, this will point to where the images will be
        /// </summary>
        public string NormalizedImageName { get; set; }

    }

    /// <summary>
    /// Different types of Items.
    /// </summary>
    public enum ItemType
    {
        /// <summary>
        /// Food itels, such as pizza, hot dogs, ...
        /// </summary>
        Food,
        /// <summary>
        /// Drinks such as water, lemonade, energy drink, ...
        /// </summary>
        Beverage
    }

    /// <summary>
    /// Contains small tools involving Items
    /// </summary>
    public class ItemTools
    {
        /// <summary>
        /// Returns an ItemType from a string. Allows for aliases.
        /// </summary>
        /// <param name="s">String</param>
        /// <returns>Corresponding ItemType. Default ItemType.Beverage.</returns>
        public static ItemType FromString(string s)
        {
            return s.ToUpper() switch
            {
                "FOOD" => ItemType.Food,
                "BEVERAGE" or _ => ItemType.Beverage
            };
        }
    }
}

